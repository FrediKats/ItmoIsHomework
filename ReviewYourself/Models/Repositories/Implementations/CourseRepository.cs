using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString;

        public CourseRepository(string connectionString)
        {
            _connectionString = connectionString;
            //_connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;
        }

        public void Create(Course course)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insert = SQL
                    .INSERT_INTO("Course (CourseID, Title, CourseDescription, MentorID)")
                    .VALUES(course.Id, course.Title, course.Description, course.Mentor)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public void CreateMember(Guid courseId, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insert = SQL
                    .INSERT_INTO("CourseMembership (UserID, CourseID, Permission)")
                    .VALUES(userId, courseId, 0)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public Course Read(Guid courseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("({0}) ON MentorID = UserID", "ResourceUser")
                    .WHERE("CourseID = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                return new Course
                {
                    Id = Guid.Parse(reader["CourseID"].ToString()),
                    Title = reader["Title"].ToString(),
                    Description = reader["CourseDescription"].ToString(),
                    Mentor = new ResourceUser
                    {
                        Id = Guid.Parse(reader["UserID"].ToString()),
                        Login = reader["UserLogin"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["UserPassword"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Biography = reader["Bio"].ToString()
                    }
                };
            }
        }

        public ICollection<Course> ReadByUser(Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("({0}) ON Course.MentorID = ResourceUser.UserID", "ResourceUser")
                    .JOIN("({0}) ON Course.CourseID = CourseMembership.CourseID",
                        SQL
                        .SELECT("CourseID")
                        .FROM("CourseMembership")
                        .WHERE("CourseMembership.UserID = {0}", userId))
                    .ToCommand(connection)
                    .ExecuteReader();

                ICollection<Course> courses = new List<Course>();

                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Id = Guid.Parse(reader["CourseID"].ToString()),
                        Title = reader["Title"].ToString(),
                        Description = reader["CourseDescription"].ToString(),
                        Mentor = new ResourceUser
                        {
                            Id = Guid.Parse(reader["UserID"].ToString()),
                            Login = reader["UserLogin"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["UserPassword"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Biography = reader["Bio"].ToString()
                        }
                    });
                }

                return courses;
            }
        }

        public void Update(Course course)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var update = SQL
                    .UPDATE("Course")
                    .SET("Title = {0}", course.Title)
                    ._("CourseDescription = {0}", course.Description)
                    .WHERE("CourseID = {0}", course.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public void Delete(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Guid courseId, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var delete = SQL
                    .DELETE_FROM("CourseMembership")
                    .WHERE("UserID = {0}", userId)
                    ._("CourseID = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }
    }
}
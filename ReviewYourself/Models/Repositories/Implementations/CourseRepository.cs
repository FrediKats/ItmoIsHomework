using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private string _connectionString;

        public static CourseRepository Create(string connectionString)
        {
            return new CourseRepository()
            {
                _connectionString = connectionString
            };
        }

        public CourseRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public void Create(Course course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                course.Id = Guid.NewGuid();

                SQL.INSERT_INTO("Course (CourseID, Title, CourseDescription, MentorID)")
                    .VALUES(course.Id, course.Title, course.Description, course.Mentor.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public void CreateMember(Guid courseId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.INSERT_INTO("CourseMembership (UserID, CourseID, Permission)")
                    .VALUES(userId, courseId, 0)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public Course Read(Guid courseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("ResourceUser ON MentorID = UserID")
                    .WHERE("CourseID = {0}", courseId)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    var course = ReaderConvertor.ToCourse(reader);
                    course.Mentor = new ResourceUser()
                    {
                        Id = Guid.Parse(reader["UserID"].ToString())
                    };

                    return course;
                    //return new Course
                    //{
                    //    Id = Guid.Parse(reader["CourseID"].ToString()),
                    //    Title = reader["Title"].ToString(),
                    //    Description = reader["CourseDescription"].ToString(),
                    //    Mentor = new ResourceUser
                    //    {
                    //        Id = Guid.Parse(reader["UserID"].ToString()),
                    //        Login = reader["UserLogin"].ToString(),
                    //        Email = reader["Email"].ToString(),
                    //        FirstName = reader["FirstName"].ToString(),
                    //        LastName = reader["LastName"].ToString(),
                    //        Biography = reader["Bio"].ToString()
                    //    }
                    //};
                }
            }
        }

        public ICollection<Course> ReadByUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("ResourceUser ON MentorID = UserID")
                    .WHERE("MentorID = {0}", userId)
                    .ToCommand(connection);

                ICollection<Course> courseList = new List<Course>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseList.Add(ReaderConvertor.ToCourse(reader));
                        //courseList.Add(new Course
                        //{
                        //    Id = Guid.Parse(reader["CourseID"].ToString()),
                        //    Title = reader["Title"].ToString(),
                        //    Description = reader["CourseDescription"].ToString(),
                        //    Mentor = new ResourceUser
                        //    {
                        //        Id = Guid.Parse(reader["UserID"].ToString()),
                        //        Login = reader["UserLogin"].ToString(),
                        //        Email = reader["Email"].ToString(),
                        //        FirstName = reader["FirstName"].ToString(),
                        //        LastName = reader["LastName"].ToString()
                        //    }
                        //});
                    }
                }

                command = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("ResourceUser ON Course.MentorID = ResourceUser.UserID")
                    .JOIN("({0}) t0 ON Course.CourseID = t0.CourseID",
                        SQL.SELECT("CourseID")
                            .FROM("CourseMembership")
                            .WHERE("CourseMembership.UserID = {0}", userId)
                    ._("Permission > 0"))
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseList.Add(ReaderConvertor.ToCourse(reader));
                        //courseList.Add(new Course
                        //{
                        //    Id = Guid.Parse(reader["CourseID"].ToString()),
                        //    Title = reader["Title"].ToString(),
                        //    Description = reader["CourseDescription"].ToString(),
                        //    Mentor = new ResourceUser
                        //    {
                        //        Id = Guid.Parse(reader["UserID"].ToString()),
                        //        Login = reader["UserLogin"].ToString(),
                        //        Email = reader["Email"].ToString(),
                        //        FirstName = reader["FirstName"].ToString(),
                        //        LastName = reader["LastName"].ToString()
                        //    }
                        //});
                    }
                }

                return courseList;
            }
        }

        public ICollection<Course> ReadInvitesByUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = SQL
                    .SELECT("*")
                    .FROM("Course")
                    .INNER_JOIN("ResourceUser ON Course.MentorID = ResourceUser.UserID")
                    .JOIN("({0}) t0 ON Course.CourseID = t0.CourseID",
                        SQL.SELECT("CourseID")
                            .FROM("CourseMembership")
                            .WHERE("CourseMembership.UserID = {0}", userId)
                    ._("Permission = 0"))
                    .ToCommand(connection);

                ICollection<Course> courseList = new List<Course>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseList.Add(ReaderConvertor.ToCourse(reader));
                        //courseList.Add(new Course
                        //{
                        //    Id = Guid.Parse(reader["CourseID"].ToString()),
                        //    Title = reader["Title"].ToString(),
                        //    Description = reader["CourseDescription"].ToString(),
                        //    Mentor = new ResourceUser
                        //    {
                        //        Id = Guid.Parse(reader["UserID"].ToString()),
                        //        Login = reader["UserLogin"].ToString(),
                        //        Email = reader["Email"].ToString(),
                        //        FirstName = reader["FirstName"].ToString(),
                        //        LastName = reader["LastName"].ToString()
                        //    }
                        //});
                    }
                }

                return courseList;
            }
        }
        
        public ICollection<ResourceUser> ReadMembersByCourse(Guid courseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .JOIN("({0}) t0 ON ResourceUser.UserID = t0.UserID",
                        SQL.SELECT("UserID")
                            .FROM("Coursemembership")
                            .WHERE("CourseID = {0}", courseId)
                            ._("Permission > {0}", 0))
                    .ToCommand(connection);

                /* if previous won't work you can use this
                string selectExpression = $"SELECT * FROM ResourceUser WHERE UserID in (SELECT UserID FROM CourseMembership WHERE CourseID = '{courseId}' AND Permission > 0)";
                SqlCommand read = new SqlCommand(selectExpression, connection);
                SqlDataReader reader = read.ExecuteReader();
                */

                ICollection<ResourceUser> memberList = new List<ResourceUser>();

                using (var reader = command.ExecuteReader())
                {
                    //TODO:
                    while (reader.Read())
                    {
                        memberList.Add(ReaderConvertor.ToUser(reader));
                        //memberList.Add(new ResourceUser
                        //{
                        //    Id = Guid.Parse(reader["UserID"].ToString()),
                        //    Login = reader["UserLogin"].ToString(),
                        //    Email = reader["Email"].ToString(),
                        //    FirstName = reader["FirstName"].ToString(),
                        //    LastName = reader["LastName"].ToString(),
                        //    Biography = reader["Bio"].ToString()
                        //});
                    }
                }

                return memberList;
            }
        }

        public ICollection<ResourceUser> ReadInvitedByCourse(Guid courseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //TODO:
                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .JOIN("({0}) t0 ON ResourceUser.UserID = t0.UserID",
                        SQL.SELECT("UserID")
                            .FROM("Coursemembership")
                            .WHERE("CourseID = {0}", courseId)
                            ._("Permission = {0}", 0))
                    .ToCommand(connection);
                    

                ICollection<ResourceUser> invitedList = new List<ResourceUser>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invitedList.Add(ReaderConvertor.ToUser(reader));
                        //invitedList.Add(new ResourceUser
                        //{
                        //    Id = Guid.Parse(reader["UserID"].ToString()),
                        //    Login = reader["UserLogin"].ToString(),
                        //    Email = reader["Email"].ToString(),
                        //    FirstName = reader["FirstName"].ToString(),
                        //    LastName = reader["LastName"].ToString(),
                        //    Biography = reader["Bio"].ToString()
                        //});
                    }
                }

                return invitedList;
            }
        }

        public void Update(Course course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.UPDATE("Course")
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.DELETE_FROM("CourseMembership")
                    .WHERE("UserID = {0}", userId)
                    ._("CourseID = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public void AcceptInvite(Guid courseId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.UPDATE("CourseMembership")
                    .SET("Permission = 1")
                    .WHERE("UserID = {0}", userId)
                    ._("CourseId = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public bool IsMember(Guid courseId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var permission = SQL
                    .SELECT("Permission")
                    .FROM("CourseMembership")
                    .WHERE("UserID = {0}", userId)
                    ._("CourseId = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteScalar();

                return (int.Parse(permission?.ToString() ?? "0") > 0);
            }
        }
    }
}
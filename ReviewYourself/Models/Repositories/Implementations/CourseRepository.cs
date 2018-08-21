using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools.DataRecordExtensions;

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
                    .WHERE("CourseID = {0}", courseId)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var course = reader.GetCourse();
                    return course;
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
                    .WHERE("MentorID = {0}", userId)
                    .ToCommand(connection);

                ICollection<Course> courseList = new List<Course>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseList.Add(reader.GetCourse());
                    }
                }

                command = SQL
                    .SELECT("*")
                    .FROM("Course")
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
                        courseList.Add(reader.GetCourse());
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
                        courseList.Add(reader.GetCourse());
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

                ICollection<ResourceUser> memberList = new List<ResourceUser>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        memberList.Add(reader.GetResourceUser());
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
                        invitedList.Add(reader.GetResourceUser());
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
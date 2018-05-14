using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
            //_connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;
        }

        public void Create(ResourceUser user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insert = SQL
                    .INSERT_INTO("ResourceUser (UserID, UserLogin, Email, UserPassword, FirstName, LastName, Bio)")
                    .VALUES(Guid.NewGuid(), user.Login, user.Email, user.Password, user.FirstName, user.LastName, user.Biography)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public ResourceUser Read(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .WHERE("UserID = {0}", id)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                return new ResourceUser
                {
                    Id = Guid.Parse(reader["UserID"].ToString()),
                    Login = reader["UserLogin"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["UserPassword"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Biography = reader["Bio"].ToString()
                };
            }
        }

        public ResourceUser ReadByUserName(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .WHERE("UserLogin = {0}", username)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                return new ResourceUser
                {
                    Id = Guid.Parse(reader["UserID"].ToString()),
                    Login = reader["UserLogin"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["UserPassword"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Biography = reader["Bio"].ToString()
                };
            }
        }

        public ICollection<ResourceUser> ReadByCourse(Guid courseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .JOIN("({0}) ON ResourceUser.UserID = CourseMembership.UserID",
                            SQL
                            .SELECT("UserID")
                            .FROM("Coursemembership")
                            .WHERE("CourseID = {0}", courseId)
                            ._("Permission > 0"))
                    .ToCommand(connection)
                    .ExecuteReader();

                /* if previous won't work you can use this
                string selectExpression = $"SELECT * FROM ResourceUser WHERE UserID in (SELECT UserID FROM CourseMembership WHERE CourseID = '{courseId}' AND Permission > 0)";
                SqlCommand read = new SqlCommand(selectExpression, connection);
                SqlDataReader reader = read.ExecuteReader();
                */

                ICollection<ResourceUser> courseMembers = new List<ResourceUser>();

                while (reader.Read())
                {
                    courseMembers.Add(new ResourceUser
                    {
                        Id = Guid.Parse(reader["UserID"].ToString()),
                        Login = reader["UserLogin"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["UserPassword"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Biography = reader["Bio"].ToString()
                    });
                }

                return courseMembers;
            }
        }

        public void Update(ResourceUser user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var update = SQL
                    .UPDATE("ResourceUser")
                    .SET("FirstName = {0}", user.FirstName)
                    ._("LastName = {0}", user.LastName)
                    ._("Bio = {0}", user.Biography)
                    .WHERE("UserID = {0}", user.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public void Delete(ResourceUser user)
        {
            //problems with foreign keys
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var delete = SQL.
                    DELETE_FROM("ResourceUser")
                    .WHERE("UserID = {0}", user.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }
    }
}
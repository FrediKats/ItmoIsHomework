using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    //TODO: don't use resharper here
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserRepository()
        {
#if DEBUG
            _connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;
#else
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
#endif
        }

        public void Create(ResourceUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
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
            using (var connection = new SqlConnection(_connectionString))
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
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Biography = reader["Bio"].ToString()
                };
            }
        }

        public ResourceUser ReadByUserName(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
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
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Biography = reader["Bio"].ToString()
                };
            }
        }

        public void Update(ResourceUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var delete = SQL.DELETE_FROM("ResourceUser")
                    .WHERE("UserID = {0}", user.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }
    }
}
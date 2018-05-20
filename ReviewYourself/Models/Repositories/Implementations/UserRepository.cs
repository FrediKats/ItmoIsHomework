using System;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Models.Repositories.Implementations
{
    //TODO: don't use resharper here
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public void Create(ResourceUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.INSERT_INTO("ResourceUser (UserID, UserLogin, Email, UserPassword, FirstName, LastName, Bio)")
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

                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .WHERE("UserID = {0}", id)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return ReaderConvertor.ToUser(reader);
                }
            }
        }

        public ResourceUser ReadByUserName(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .WHERE("UserLogin = {0}", username)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return ReaderConvertor.ToUser(reader);
                }
            }
        }

        public void Update(ResourceUser user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.UPDATE("ResourceUser")
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

                SQL.DELETE_FROM("ResourceUser")
                    .WHERE("UserID = {0}", user.Id)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public static UserRepository Create(string connectionString)
        {
            return new UserRepository
            {
                _connectionString = connectionString
            };
        }
    }
}
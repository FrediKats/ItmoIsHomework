using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public void Create(ResourceUser user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
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
            string connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceUser")
                    .WHERE($"UserID = {id}")
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                return new ResourceUser()
                {
                    Id = (Guid) reader["UserID"],
                    Login = (string) reader["UserLogin"],
                    Email = (string) reader["Email"],
                    Password = (string) reader["UserPassword"],
                    FirstName = (string) reader["FirstName"],
                    LastName = (string) reader["LastName"],
                    Biography = (string) reader["Bio"]
                };
            }
        }

        public ResourceUser ReadByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<ResourceUser> ReadByCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void Update(ResourceUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(ResourceUser user)
        {
            throw new NotImplementedException();
        }
    }
}
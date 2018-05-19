using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private string _connectionString;

        public static TokenRepository Create(string connectionString)
        {
            return new TokenRepository()
            {
                _connectionString =  connectionString
            };
        }

        public TokenRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public Token GenerateToken(string username, string password)
        {
            //TODO: get user from UseTable, check if password is same

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("UserId, UserPassword")
                    .FROM("ResourceUser")
                    .WHERE("UserLogin = {0}", username)
                    .ToCommand(connection)
                    .ExecuteReader();

                //TODO: Exception if two or more results
                reader.Read();

                if (password != reader["UserPassword"].ToString())
                    return null;

                return new Token()
                {
                    TokenData = Guid.NewGuid(),
                    UserId = Guid.Parse(reader["UserID"].ToString())
                };
            }
        }

        public void DisableToken(Token token)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken(Token token)
        {
            //TODO: add validation
            return true;
            throw new NotImplementedException();
        }
    }
}
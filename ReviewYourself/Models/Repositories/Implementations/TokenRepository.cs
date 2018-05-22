using DbExtensions;
using System;
using System.Configuration;
using System.Data.SqlClient;

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
            //TODO: get user from UserTable, check if password is same

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("UserId, UserPassword")
                    .FROM("ResourceUser")
                    .WHERE("UserLogin = {0}", username)
                    .ToCommand(connection);

                Token token;

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    if (password != reader.GetString("UserPassword")) return null;

                    token = new Token()
                    {
                        TokenData = Guid.NewGuid(),
                        UserId = Guid.Parse(reader["UserID"].ToString())
                    };
                }

                SQL.INSERT_INTO("Token (TokenData, UserID)")
                    .VALUES(token.TokenData, token.UserId);

                return token;
            }
        }

        public void DisableToken(Token token)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.DELETE_FROM("Token")
                    .WHERE("TokenData = {0}", token.TokenData)
                    ._("UserID = {0}", token.UserId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public bool ValidateToken(Token token)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var count = SQL
                    .SELECT("Count(*)")
                    .FROM("Token")
                    .WHERE("TokenData = {0}", token.TokenData)
                    ._("UserID = {0}", token.UserId)
                    .ToCommand(connection)
                    .ExecuteScalar();

                return int.Parse(count.ToString()) == 1;
            }
        }
    }
}
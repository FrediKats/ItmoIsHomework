using System;
using System.Linq;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private static readonly string _randomString;

        static TokenRepository()
        {
            _randomString = GenerateRandomString();
        }

        private static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, chars.Length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Token GenerateToken(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void DisableToken(Token token)
        {
            throw new NotImplementedException();
        }

        public ResourceUser GetUserByToken(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
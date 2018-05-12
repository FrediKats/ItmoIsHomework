using System;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        public void GenerateToken(Guid userId)
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
using System;

namespace ReviewYourself.Models.Repositories
{
    public interface ITokenRepository
    {
        void GenerateToken(Guid userId);
        void DisableToken(Token token);
        ResourceUser GetUserByToken(Token token);
    }
}
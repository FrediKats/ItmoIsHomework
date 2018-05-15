namespace ReviewYourself.Models.Repositories
{
    public interface ITokenRepository
    {
        Token GenerateToken(string username, string login);
        void DisableToken(Token token);
        ResourceUser GetUserByToken(Token token);
    }
}
namespace ReviewYourself.Models.Repositories
{
    public interface ITokenRepository
    {
        Token GenerateToken(string username, string login);
        void DisableToken(Token token);
        bool ValidateToken(Token token);
    }
}
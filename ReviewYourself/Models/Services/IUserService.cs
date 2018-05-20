using System;

namespace ReviewYourself.Models.Services
{
    public interface IUserService
    {
        Token SignIn(string login, string password);
        void SignOut(Token token);
        void SignUp(ResourceUser user);

        ResourceUser GetUser(Token token, Guid userId);
        ResourceUser GetUserByUsername(Token token, string username);
        void UpdateUser(Token token, ResourceUser user);
    }
}
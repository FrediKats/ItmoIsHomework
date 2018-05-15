using System;

namespace ReviewYourself.Models.Services
{
    public interface IUserService
    {
        Token SignIn(string login, string password);
        void SignOut(Token token);
        void SignUp(string login, string password, ResourceUser user);

        ResourceUser GetUser(Guid userId, Token token);
        ResourceUser FindUserByUsername(string username, Token token);
        void UpdateUser(ResourceUser user, Token token);
    }
}
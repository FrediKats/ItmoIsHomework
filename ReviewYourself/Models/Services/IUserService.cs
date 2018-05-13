using System;

namespace ReviewYourself.Models.Services
{
    public interface IUserService
    {
        Token SignIn(string login, string password);
        void SignOut(Token token);
        //TODO: think about it
        void SignUp(string login, string password);
        ResourceUser GetUser(Guid userId);
        ResourceUser FindUserByUsername(string username);
        void UpdateUser(ResourceUser user, Token token);
    }
}
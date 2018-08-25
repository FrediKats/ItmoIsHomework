using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Services
{
    public interface IAuthorizationService
    {
        Token RegisterMember(RegistrationData data);
        void LogOut(Token token);
        Token LogIn(AuthorizeData authData);
        bool IsUsernameAvailable(string username);
    }
}
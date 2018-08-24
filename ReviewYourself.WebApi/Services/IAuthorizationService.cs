using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Services
{
    public interface IAuthorizationService
    {
        void RegisterMember(RegistrationData registrationData);
        //TODO: implement
        //void LogOut(Token token);

        Token LogIn(AuthorizeData authData);
        bool IsUsernameAvaliable(string username);
    }
}
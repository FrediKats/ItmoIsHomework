using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Services
{
    public interface IPeerReviewAuthService
    {
        UserToken RegisterMember(RegistrationData data);
        void LogOut(UserToken token);
        UserToken LogIn(AuthData authData);
        bool IsUsernameAvailable(string username);
    }
}
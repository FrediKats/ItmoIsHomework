using System;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Services
{
    public interface IAuthorizationService
    {
        (string Token, Guid UserId) RegisterMember(RegistrationData data);
        void LogOut(Token token);
        (string Token, Guid UserId) LogIn(AuthorizeData authData);
        bool IsUsernameAvailable(string username);
    }
}
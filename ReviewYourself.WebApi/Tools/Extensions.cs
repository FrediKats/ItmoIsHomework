using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Tools
{
    public static class Extensions
    {
        public static User ToUser(this RegistrationData data)
        {
            return new User
            {
                Login = data.Login,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName
            };
        }
    }
}
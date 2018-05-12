using System;

namespace ReviewYourself.Models.Services.Implementations
{
    public class UserService : IUserService
    {
        public Token SignIb(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void SignOut(Token token)
        {
            throw new NotImplementedException();
        }

        public void SignIn(string login, string password)
        {
            throw new NotImplementedException();
        }

        public ResourceUser GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public ResourceUser FindUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(ResourceUser user, Token token)
        {
            throw new NotImplementedException();
        }
    }
}
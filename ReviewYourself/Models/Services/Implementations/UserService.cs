using System;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }
        public Token SignIn(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void SignOut(Token token)
        {
            throw new NotImplementedException();
        }

        public void SignUp(string login, string password)
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
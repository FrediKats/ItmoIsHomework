using System;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }
        public Token SignIn(string login, string password)
        {
            //TODO: user info
            //_tokenRepository.GetUserByToken();
            throw new NotImplementedException();
        }

        public void SignOut(Token token)
        {
            _tokenRepository.DisableToken(token);
            throw new NotImplementedException();
        }

        //TODO: add userInfo
        public void SignUp(string login, string password)
        {
            throw new NotImplementedException();
        }

        public ResourceUser GetUser(Guid userId)
        {
            //TODO: Add token?
            return _userRepository.Read(userId);
            throw new NotImplementedException();

        }

        public ResourceUser FindUserByUsername(string username)
        {
            return _userRepository.ReadByUserName(username);
        }

        public void UpdateUser(ResourceUser user, Token token)
        {
            _userRepository.Update(user);
        }
    }
}
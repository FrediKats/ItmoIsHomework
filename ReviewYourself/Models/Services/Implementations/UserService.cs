using System;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

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
        public void SignUp(string login, string password, ResourceUser user)
        {
            throw new NotImplementedException();
        }

        public ResourceUser GetUser(Token token, Guid userId)
        {
            return _userRepository.Read(userId);
        }

        public ResourceUser FindUserByUsername(Token token, string username)
        {
            return _userRepository.ReadByUserName(username);
        }

        public void UpdateUser(Token token, ResourceUser user)
        {
            _userRepository.Update(user);
        }
    }
}
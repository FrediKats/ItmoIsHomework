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
            return _tokenRepository.GenerateToken(login, password);
        }

        public void SignOut(Token token)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            _tokenRepository.DisableToken(token);
        }

        public void SignUp(string login, string password, ResourceUser user)
        {
            _userRepository.Create(user);
            throw new NotImplementedException();
        }

        public ResourceUser GetUser(Token token, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _userRepository.Read(userId);
        }

        public ResourceUser FindUserByUsername(Token token, string username)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _userRepository.ReadByUserName(username);
        }

        public void UpdateUser(Token token, ResourceUser user)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            if (token.UserId != user.Id)
            {
                throw new Exception("Different userId and token");
            }

            _userRepository.Update(user);
        }
    }
}
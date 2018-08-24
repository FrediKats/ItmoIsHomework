using System;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;
using System.IdentityModel.Tokens.Jwt;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly PeerReviewContext _context;
        private readonly IJwtTokenFactory _tokenFactory;

        public AuthorizationService(PeerReviewContext context,
            IJwtTokenFactory tokenFactory)
        {
            _context = context;
            _tokenFactory = tokenFactory;
        }

        public Token RegisterMember(RegistrationData data)
        {
            if (IsUsernameAvailable(data.Login))
            {
                throw new Exception();
            }


            var user = ToUser(data);
            _context.Users.Add(user);
            _context.AuthorizeDatas.Add(new AuthorizeData {Login = data.Login, Password = data.Password});
            var jwtToken = _tokenFactory.CreateJwtToken(user.Id);
            var token = new Token() {AccessToken = jwtToken, UserId = user.Id};
            _context.Tokens.Add(token);
            _context.SaveChanges();

            return token;
        }

        public void LogOut(Token token)
        {
            var tokenToRemove = _context.Tokens
                .First(t => t.AccessToken == token.AccessToken);
            _context.Tokens.Remove(tokenToRemove);

            _context.SaveChanges();
        }

        public Token LogIn(AuthorizeData authData)
        {
            if (_context.AuthorizeDatas.Any(ad => ad.Login == authData.Login
                                                  && ad.Password == authData.Password))
            {
                var user = _context.Users
                    .First(u => u.Login == authData.Login);
                var jwtToken = _tokenFactory.CreateJwtToken(user.Id);
                var token = new Token() { AccessToken = jwtToken, UserId = user.Id };
                _context.Tokens.Add(token);
                return token;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            return _context.Users.Any(u => u.Login == username);
        }

        private User ToUser(RegistrationData data)
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
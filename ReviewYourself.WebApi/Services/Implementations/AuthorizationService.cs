using System.Data;
using System.Linq;
using System.Security.Authentication;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;

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
            if (IsUsernameAvailable(data.Login) == false)
            {
                throw new DuplicateNameException(data.Login);
            }

            var user = data.ToUser();
            _context.Users.Add(user);
            var jwtToken = _tokenFactory.CreateJwtToken(user.Id);
            var token = new Token {AccessToken = jwtToken, UserId = user.Id};
            var authData = new AuthorizeData {Login = data.Login, Password = data.Password};

            _context.AuthorizeDatas.Add(authData);
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
            if (!_context.AuthorizeDatas.Any(ad => ad.Login == authData.Login
                                                   && ad.Password == authData.Password))
            {
                throw new AuthenticationException("Invalid login or password");
            }

            var user = _context.Users.First(u => u.Login == authData.Login);
            var jwtToken = _tokenFactory.CreateJwtToken(user.Id);
            var token = new Token {AccessToken = jwtToken, UserId = user.Id};

            _context.Tokens.Add(token);
            _context.SaveChanges();

            return token;
        }

        public bool IsUsernameAvailable(string username)
        {
            return _context.Users.Any(u => u.Login == username) == false;
        }
    }
}
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly PeerReviewContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtTokenFactory _tokenFactory;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationService(PeerReviewContext context,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IJwtTokenFactory tokenFactory)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }

        public (string Token, Guid UserId) RegisterMember(RegistrationData data)
        {
            var user = new IdentityUser(data.Login);
            var result = _userManager.CreateAsync(user, data.Password).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().ToString());
            }

            _signInManager.SignInAsync(user, true);
            var token = _tokenFactory.CreateJwtToken(Guid.Parse(user.Id));

            _context.Users.Add(ToUser(data, Guid.Parse(user.Id)));
            _context.AuthorizeDatas.Add(new AuthorizeData {Login = data.Login, Password = data.Password});
            _context.SaveChanges();

            return (token, Guid.Parse(user.Id));
        }

        public void LogOut(Token token)
        {
            throw new NotImplementedException();
        }

        public (string Token, Guid UserId) LogIn(AuthorizeData authData)
        {
            var result = _signInManager.PasswordSignInAsync(
                    authData.Login, authData.Password, false, false)
                .Result;

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            var user = _userManager.FindByNameAsync(authData.Login).Result;
            var token = _tokenFactory.CreateJwtToken(Guid.Parse(user.Id));
            return (token, Guid.Parse(user.Id));
        }

        public bool IsUsernameAvailable(string username)
        {
            return _context.Users.Any(u => u.Login == username);
        }

        private User ToUser(RegistrationData data, Guid id)
        {
            return new User
            {
                Id = id,
                Login = data.Login,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName
            };
        }
    }
}
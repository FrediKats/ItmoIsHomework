using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class PeerReviewAuthService : IPeerReviewAuthService
    {
        private readonly PeerReviewContext _context;

        public PeerReviewAuthService(PeerReviewContext context)
        {
            _context = context;
        }

        public UserToken RegisterMember(RegistrationData data)
        {
            if (IsUsernameAvailable(data.Login) == false)
            {
                throw new DuplicateNameException(data.Login);
            }

            PeerReviewUser peerReviewUser = data.ToUser();
            _context.Users.Add(peerReviewUser);

            var authData = new AuthData {Login = data.Login, Password = data.Password};
            _context.AuthorizeDatas.Add(authData);
            _context.SaveChanges();

            return LogIn(authData);
        }

        public void LogOut(UserToken token)
        {
            throw new NotImplementedException();
        }

        public UserToken LogIn(AuthData authData)
        {
            ClaimsIdentity identity = GetIdentity(authData.Login, authData.Password);
            if (identity == null)
            {
                throw new AuthenticationException("Invalid login or password");
            }

            PeerReviewUser user = _context.Users.First(u => u.Login == authData.Login);

            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserToken
            {
                AccessToken = encodedJwt,
                UserId = user.Id
            };
        }

        public bool IsUsernameAvailable(string username)
        {
            return _context.Users.Any(u => u.Login == username) == false;
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            AuthData person = _context.AuthorizeDatas.First(ad => ad.Login == username && ad.Password == password);
            if (person == null)
            {
                return null;
            }

            PeerReviewUser user = _context.Users.First(u => u.Login == username);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
                //TODO: implement role system
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
            };
            var claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
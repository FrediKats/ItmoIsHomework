using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.Tools
{
    public class JwtIssuerOptions
    {
        public const string Issuer = "TemplateIssuer";
        public const string Audience = "TemplateAudience";
        public const string Key = "RandomKey";
        public const int Lifetime = 10;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static Token GenerateToken(string login, string password)
        {
            var identity = GetIdentity(login, password);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                Issuer,
                Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(Lifetime)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token
            {
                AccessToken = encodedJwt,
                UserId = identity.Claims.Select(s => Guid.Parse(s.Value)).First()
            };
        }

        private static ClaimsIdentity GetIdentity(string username, string password)
        {
            var person = new User();
            if (person == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            return claimsIdentity;
        }
    }
}
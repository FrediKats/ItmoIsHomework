using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ReviewYourself.WebApi.Tools
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private static readonly Random Random = new Random((int) DateTime.UtcNow.Ticks);

        public string CreateJwtToken(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException();
            }

            return new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: CreateClaimsIdentityFor(id.ToString()).Claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: CreateSigningCredentials(GenerateKey())
                ));
        }

        private static string GenerateKey(int size = 23)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var res = Enumerable.Range(1, size).Select(s => chars[Random.Next(chars.Length)]).ToArray();
            return new string(res);
        }

        private static SigningCredentials CreateSigningCredentials(string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            return new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );
        }

        private static ClaimsIdentity CreateClaimsIdentityFor(string id)
        {
            var claim = new Claim(ClaimsIdentity.DefaultNameClaimType, id);
            return new ClaimsIdentity(
                new[] {claim}, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
        }
    }
}
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ReviewYourself.WebApi.Tools
{
    public class JwtTokenFactory : IJwtTokenFactory
    {
        private const string Key = "q7fs8DDw823hSyaNYCKsa02";

        public string CreateJwtToken(Guid id)
        {
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: CreateClaimsIdentityFor(id.ToString()).Claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: CreateSigningCredentials(Key),
                notBefore: DateTime.Now
            ));
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
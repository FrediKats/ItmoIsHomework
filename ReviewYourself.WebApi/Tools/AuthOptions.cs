using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ReviewYourself.WebApi.Tools
{
    public class AuthOptions
    {
        public const string Issuer = "SimpleJwtFactory";
        public const string Audience = "PeerReview";
        private const string Key = "some-strong-key";
        public const int Lifetime = 60 * 24 * 7;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
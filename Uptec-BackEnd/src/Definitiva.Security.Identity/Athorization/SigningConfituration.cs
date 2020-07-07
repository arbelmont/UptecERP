using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Definitiva.Security.Identity.Athorization
{
    public class SigningConfituration
    {
        private const string SecretKey = "e60564de-b530-4d24-9b43-74fe0db4d4af";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public SigningCredentials SigningCredentials { get; }

        public SigningConfituration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CmnSoftwareBackend.Shared.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}

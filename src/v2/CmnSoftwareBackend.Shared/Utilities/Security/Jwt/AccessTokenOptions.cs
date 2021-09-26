using System;
using System.Collections.Generic;

namespace CmnSoftwareBackend.Shared.Utilities.Security.Jwt
{
    public class AccessTokenOptions
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}

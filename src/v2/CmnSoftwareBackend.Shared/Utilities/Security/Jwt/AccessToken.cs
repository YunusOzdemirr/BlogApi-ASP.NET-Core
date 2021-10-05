using System;
namespace CmnSoftwareBackend.Shared.Utilities.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; } = DateTime.Now.AddMinutes(30);
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; } = DateTime.Now.AddHours(1);
    }
}

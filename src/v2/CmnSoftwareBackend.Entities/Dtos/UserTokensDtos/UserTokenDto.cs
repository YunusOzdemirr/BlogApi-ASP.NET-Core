using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserTokensDtos
{
    public class UserTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}

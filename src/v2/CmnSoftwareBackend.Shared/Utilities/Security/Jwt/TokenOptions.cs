using System;       
namespace CmnSoftwareBackend.Shared.Utilities.Security.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; } = "https://localhost:3000";
        public string Issuer { get; set; } = "Yunus@yunus.com";
        public int AccessTokenExpiration { get; set; } = 30;
        public string SecurityKey { get; set; } = "ZXmP5FSQvf2TmPugnFr5CbXW3GjgcmAULVBzaxkH";
    }
}
    
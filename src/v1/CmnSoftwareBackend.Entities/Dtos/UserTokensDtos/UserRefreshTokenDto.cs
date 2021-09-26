using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CmnSoftwareBackend.Entities.Dtos.UserTokensDtos
{
    public class UserRefreshTokenDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}

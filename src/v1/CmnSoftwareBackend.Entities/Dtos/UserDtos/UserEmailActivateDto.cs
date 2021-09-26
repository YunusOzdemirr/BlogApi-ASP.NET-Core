using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserEmailActivateDto
    {
        public string EmailAddress { get; set; }
        public string ActivationCode { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}

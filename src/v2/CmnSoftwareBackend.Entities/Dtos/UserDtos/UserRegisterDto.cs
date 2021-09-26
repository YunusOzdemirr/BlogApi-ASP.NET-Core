using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CmnSoftwareBackend.Entities.ComplexTypes;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Base64File { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}

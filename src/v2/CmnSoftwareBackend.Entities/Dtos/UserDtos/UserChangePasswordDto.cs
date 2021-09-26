using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserChangePasswordDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}

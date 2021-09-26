using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos;
using CmnSoftwareBackend.Entities.Dtos.UserPictureUpdateDtos;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailAddressVerified { get; set; }
        public string Picture { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? LastLogin { get; set; }
        public Guid? UserPictureId { get; set; }
        public UserPictureDto UserPicture { get; set; }
        public IEnumerable<UserNotificationDto> UserNotifications { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

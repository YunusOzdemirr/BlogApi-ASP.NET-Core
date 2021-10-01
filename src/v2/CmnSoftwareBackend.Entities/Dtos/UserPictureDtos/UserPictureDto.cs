using System;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;

namespace CmnSoftwareBackend.Entities.Dtos.UserPictureDtos
{
    public class UserPictureDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public string File { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}

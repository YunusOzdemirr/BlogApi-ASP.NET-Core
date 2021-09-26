using System;
using CmnSoftwareBackend.Entities.ComplexTypes;

namespace CmnSoftwareBackend.Entities.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        //EKLEME
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Gender Gender { get; set; }
        public Guid? UserPictureId { get; set; }
        public string Base64File { get; set; }
        public bool IsActive { get; set; }
        public Guid ModifiedByUserId { get; set; }
    }
}

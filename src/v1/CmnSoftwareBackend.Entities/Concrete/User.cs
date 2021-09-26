using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class User:EntityBase<Guid,Guid>,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public byte PhoneNumber{ get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailAddressVerified { get; set; }
        public string VerificationCode { get; set; }
        public Guid? UserPictureId { get; set; }
        public UserPicture UserPicture { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<CommentWithUser> CommentWithUsers{ get; set; }
    }
}
using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos
{
    public class UserNotificationUpdateDto
    {
        public int Id { get; set; }
        public Guid UserId{ get; set; }
        public string Message { get; set; }
    }
}

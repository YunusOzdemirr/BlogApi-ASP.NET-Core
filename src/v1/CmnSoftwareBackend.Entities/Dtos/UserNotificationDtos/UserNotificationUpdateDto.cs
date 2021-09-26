using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos
{
    public class UserNotificationUpdateDto
    {
        public int Id { get; set; }
        public int UserId{ get; set; }
        public string Message { get; set; }
    }
}

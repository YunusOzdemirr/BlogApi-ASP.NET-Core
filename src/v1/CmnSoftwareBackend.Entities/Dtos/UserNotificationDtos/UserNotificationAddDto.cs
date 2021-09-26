using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos
{
    public class UserNotificationAddDto
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}

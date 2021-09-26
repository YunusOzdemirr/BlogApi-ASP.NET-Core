using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos
{
    public class UserNotificationListDto
    {
        public IList<UserNotification> UserNotifications{ get; set; }
    }
}

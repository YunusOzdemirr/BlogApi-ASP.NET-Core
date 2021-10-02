using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos
{
    public class UserNotificationListDto:DtoGetBase
    {
        public IList<UserNotification> UserNotifications{ get; set; }
    }
}

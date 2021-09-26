using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserNotificationDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class UserNotificationProfile:global::AutoMapper.Profile
    {
        public UserNotificationProfile()
        {
            CreateMap<UserNotificationAddDto, UserNotification>();
            CreateMap<UserNotificationUpdateDto, UserNotification>();
            CreateMap<UserNotificationDeleteDto, UserNotification>();
        }
    }
}

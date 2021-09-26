using System;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class UserNotification:EntityBase<int>,IEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }
}

using System;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class UserPicture:EntityBase<int>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public byte[] File { get; set; }
    }
}

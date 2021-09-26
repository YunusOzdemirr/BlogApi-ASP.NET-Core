using System;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class UserOperationClaim:IEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}

using System;
namespace CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos
{
    public class UserOperationClaimAddDto
    {
        public Guid UserId { get; set; }
        public int OperationClaimId{ get; set; }
    }
}

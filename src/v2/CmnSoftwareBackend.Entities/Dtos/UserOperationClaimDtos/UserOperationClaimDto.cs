using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos
{
    public class UserOperationClaimDto
    {
        public Guid UserId{ get; set; }
        public IEnumerable<OperationClaim> OperationClaims { get; set; }
    }
}

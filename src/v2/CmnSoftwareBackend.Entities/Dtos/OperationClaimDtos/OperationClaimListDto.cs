using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos
{
    public class OperationClaimListDto:DtoGetBase
    {
        public IEnumerable<OperationClaim> OperationClaims { get; set; }
    }
}

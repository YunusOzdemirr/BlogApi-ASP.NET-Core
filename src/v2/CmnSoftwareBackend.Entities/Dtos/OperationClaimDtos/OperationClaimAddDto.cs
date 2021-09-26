using System;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos
{
    public class OperationClaimAddDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}

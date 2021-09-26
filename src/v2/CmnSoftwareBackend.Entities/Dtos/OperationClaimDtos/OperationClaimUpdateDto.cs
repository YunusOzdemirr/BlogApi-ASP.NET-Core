using System;
namespace CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos
{
    public class OperationClaimUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}

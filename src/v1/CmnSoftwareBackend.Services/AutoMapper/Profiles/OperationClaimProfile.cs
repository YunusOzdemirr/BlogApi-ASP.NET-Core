using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class OperationClaimProfile:global::AutoMapper.Profile
    {
        public OperationClaimProfile()
        {
            CreateMap<OperationClaimDto, OperationClaim>();
            CreateMap<OperationClaimUpdateDto, OperationClaim>();
        }
    }
}

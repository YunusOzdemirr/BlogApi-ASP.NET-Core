using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class UserOperationClaimProfile:Profile
    {
        public UserOperationClaimProfile()
        {
            CreateMap<UserOperationClaimAddDto, UserOperationClaimDto>();
            CreateMap<UserOperationClaimUpdateDto, UserOperationClaimDto>();
            CreateMap<UserOperationClaimDeleteDto, UserOperationClaimDto>();

            CreateMap<UserOperationClaimAddDto, UserOperationClaim>();
            CreateMap<UserOperationClaimUpdateDto, UserOperationClaim>();
            CreateMap<UserOperationClaimDeleteDto, UserOperationClaim>();
        }
    }
}


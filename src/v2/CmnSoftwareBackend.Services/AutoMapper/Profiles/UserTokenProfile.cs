using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserTokensDtos;
using CmnSoftwareBackend.Shared.Utilities.Security.Jwt;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class UserTokenProfile:Profile
    {
        public UserTokenProfile()
        {
            CreateMap<UserToken, AccessToken>().ReverseMap();
        }
    }
}

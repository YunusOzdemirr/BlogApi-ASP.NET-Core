using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Shared.Utilities.Generators;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class UserProfile:global::AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDto, User>().ForMember(dest => dest.LastLogin, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.VerificationCode, opt => opt.MapFrom(x => VerificationCodeGenerator.Generate()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now)).ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()));

            CreateMap<UserUpdateDto, User>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));

            CreateMap<User, UserDto>();
        }
    }
}

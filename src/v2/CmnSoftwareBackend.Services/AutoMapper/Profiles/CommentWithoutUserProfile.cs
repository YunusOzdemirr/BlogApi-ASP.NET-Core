using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class CommentWithoutUserProfile:Profile
    {
        public CommentWithoutUserProfile()
        {
            CreateMap<CommentWithoutUserDto, CommentWithoutUser>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.IsActive, x => x.MapFrom(x => true)).ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));
            CreateMap<CommentWithoutUserUpdateDto, CommentWithoutUser>().ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now));

        }
    }
}


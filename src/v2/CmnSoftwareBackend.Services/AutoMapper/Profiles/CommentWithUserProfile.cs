using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class CommentWithUserProfile:Profile
    {
        public CommentWithUserProfile()
        {
            //    CreateMap<ArticleAddDto, Article>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.IsActive, x => x.MapFrom(x => true)).ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));
            //    CreateMap<ArticleUpdateDto, Article>().ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now));
            //    CreateMap<Article, ArticleDto>();
            //    CreateMap<ArticleDto, Article>();
            //
            CreateMap<CommentWithUserDto, CommentWithUser>().ForMember(dest=>dest.CreatedDate,x=>x.MapFrom(x=>DateTime.Now)).ForMember(dest=>dest.ModifiedDate,x=>x.MapFrom(x=>DateTime.Now)).ForMember(dest=>dest.IsActive,x=>x.MapFrom(x=>true)).ForMember(dest=>dest.IsDeleted,x=>x.MapFrom(x=>false));
            CreateMap<CommentWithUserUpdateDto, CommentWithoutUser>().ForMember(dest=>dest.ModifiedDate,x=>x.MapFrom(x=>DateTime.Now));
            
        }
    }
}

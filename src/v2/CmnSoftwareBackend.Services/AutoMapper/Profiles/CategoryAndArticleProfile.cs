using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class CategoryAndArticleProfile : Profile
    {
        public CategoryAndArticleProfile()
        {
            CreateMap<CategoryAndArticleAddDto, CategoryAndArticle>();
            CreateMap<CategoryAndArticlesUpdateDto, CategoryAndArticle>();

            CreateMap<CategoryAndArticle, Category>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.IsActive, x => x.MapFrom(x => true)).ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));
            CreateMap<CategoryAndArticle, Article>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.IsActive, x => x.MapFrom(x => true)).ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));
        }
    }
}


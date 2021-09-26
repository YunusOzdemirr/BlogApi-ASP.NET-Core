using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class CategoryProfile:global::AutoMapper.Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.IsActive, x => x.MapFrom(x => true)).ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now));
            CreateMap<Category, CategoryDto>();
        }
    }
}

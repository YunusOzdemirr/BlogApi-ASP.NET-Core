using System;
using AutoMapper;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;

namespace CmnSoftwareBackend.Services.AutoMapper.Profiles
{
    public class ArticlePictureProfile : Profile
    {
        public ArticlePictureProfile()
        {
            CreateMap<ArticlePictureAddDto, ArticlePicture>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(x => DateTime.Now)).ForMember(dest => dest.ModifiedDate, x => x.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.IsActive, x => x.MapFrom(x => true))
                .ForMember(dest => dest.IsDeleted, x => x.MapFrom(x => false));

            CreateMap<ArticlePictureUpdateDto, ArticlePicture>().ForMember(dest=>dest.ModifiedDate,x=>x.MapFrom(x=>DateTime.Now));
            CreateMap<ArticlePictureDto, ArticlePicture>();
        }
    }
}


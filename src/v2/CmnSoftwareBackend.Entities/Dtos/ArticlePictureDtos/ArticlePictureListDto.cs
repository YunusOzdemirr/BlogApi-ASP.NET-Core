using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos
{
    public class ArticlePictureListDto:DtoGetBase
    {
        public IEnumerable<ArticlePictureDto> ArticlePictures { get; set; }
    }
}

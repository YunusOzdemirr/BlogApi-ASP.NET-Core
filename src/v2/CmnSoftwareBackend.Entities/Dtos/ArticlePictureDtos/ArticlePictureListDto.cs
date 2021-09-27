using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos
{
    public class ArticlePictureListDto:DtoGetBase
    {
        public IEnumerable<ArticlePicture> ArticlePictures { get; set; }
    }
}

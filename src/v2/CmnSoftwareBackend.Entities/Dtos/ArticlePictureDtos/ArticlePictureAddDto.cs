using System;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos
{
    public class ArticlePictureAddDto
    {
        public int ArticleId { get; set; }
        public byte[] File { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}

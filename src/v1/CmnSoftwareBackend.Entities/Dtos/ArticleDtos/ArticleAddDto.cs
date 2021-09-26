using System;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;

namespace CmnSoftwareBackend.Entities.Dtos.ArticleDtos
{
    public class ArticleAddDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public sbyte StarAverage { get; set; }
        public int TotalStarCount { get; set; }
        public Guid UserId { get; set; }
        public int ArticlePictureId { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}

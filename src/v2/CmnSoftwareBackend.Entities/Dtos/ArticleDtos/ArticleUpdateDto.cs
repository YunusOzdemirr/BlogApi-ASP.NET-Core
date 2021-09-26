using System;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.ArticleDtos
{
    public class ArticleUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public sbyte StarAverage { get; set; }
        public int TotalStarCount { get; set; }
        //articlePicture id
        public int ArticlePictureId { get; set; }
        public Guid ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

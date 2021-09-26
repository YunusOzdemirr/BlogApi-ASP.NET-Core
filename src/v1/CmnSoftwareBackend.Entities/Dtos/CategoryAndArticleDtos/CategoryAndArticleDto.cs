using System;
using CmnSoftwareBackend.Entities.Concrete;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos
{
    public class CategoryAndArticleDto
    {
        public int CateogryId { get; set; }
        public Category Category { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}

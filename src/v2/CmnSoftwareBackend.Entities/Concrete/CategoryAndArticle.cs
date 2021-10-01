using System;
namespace CmnSoftwareBackend.Entities.Concrete
{
    public class CategoryAndArticle
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}

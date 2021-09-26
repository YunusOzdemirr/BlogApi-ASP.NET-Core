using System;
namespace CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos
{
    public class CategoryAndArticlesUpdateDto
    {
        public int Id { get; set; }
        public int CateogryId { get; set; }
        public int ArticleId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos
{
    public class CategoryAndArticleListDto
    {
        public IEnumerable<CategoryAndArticleDto> CategoryAndArticles { get; set; }
    }
}

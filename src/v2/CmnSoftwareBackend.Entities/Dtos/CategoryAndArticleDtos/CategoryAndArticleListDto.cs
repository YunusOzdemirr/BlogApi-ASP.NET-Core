using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos
{
    public class CategoryAndArticleListDto : DtoGetBase
    {
        public IEnumerable<CategoryAndArticleDto> CategoryAndArticles { get; set; }
    }
}

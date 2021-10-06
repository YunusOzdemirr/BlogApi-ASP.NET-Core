using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos
{
    public class CategoryAndArticleListDto : DtoGetBase
    {
        public IEnumerable<CategoryAndArticle> CategoryAndArticles { get; set; }
    }
}

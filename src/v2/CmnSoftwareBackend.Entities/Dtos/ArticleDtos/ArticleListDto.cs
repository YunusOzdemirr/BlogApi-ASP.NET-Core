using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Dtos.ArticleDtos
{
    public class ArticleListDto:DtoGetBase
    {
        public IEnumerable<ArticleDto> Articles { get; set; }
    }
}

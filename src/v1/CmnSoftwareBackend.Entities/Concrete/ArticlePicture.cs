using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class ArticlePicture:EntityBase<int,Guid>, IEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public byte[] File { get; set; }
    }
}

using System;
using System.Collections.Generic;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class Category:EntityBase<int>,IEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int RankId { get; set; }
        public Rank Rank { get; set; }
        public ICollection<CategoryAndArticle> CategoryAndArticles { get; set; }
        
    }
}

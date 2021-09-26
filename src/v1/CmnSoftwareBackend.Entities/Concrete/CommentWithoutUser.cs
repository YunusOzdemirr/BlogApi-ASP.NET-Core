using System;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class CommentWithoutUser:EntityBase<int,int>,IEntity
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}

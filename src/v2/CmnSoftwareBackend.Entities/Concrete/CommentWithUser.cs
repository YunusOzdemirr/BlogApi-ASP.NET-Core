using System;
using CmnSoftwareBackend.Shared.Entities.Abstract;

namespace CmnSoftwareBackend.Entities.Concrete
{
    public class CommentWithUser : EntityBase<int>, IEntity
    {
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}

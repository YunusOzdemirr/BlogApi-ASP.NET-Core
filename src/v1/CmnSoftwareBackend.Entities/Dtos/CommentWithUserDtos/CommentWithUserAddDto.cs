using System;
namespace CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos
{
    public class CommentWithUserAddDto
    {
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public Guid UserId { get; set; }
        public int ArticleId { get; set; }
        public Guid? CreatedByUserId { get; set; }
    }
}

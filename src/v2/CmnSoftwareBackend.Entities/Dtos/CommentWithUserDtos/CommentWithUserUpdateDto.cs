using System;
namespace CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos
{
    public class CommentWithUserUpdateDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public Guid UserId { get; set; }
        public int ArticleId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

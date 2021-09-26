using System;
namespace CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos
{
    public class CommentWithoutUserUpdateDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public int ArticleId { get; set; }
    }
}

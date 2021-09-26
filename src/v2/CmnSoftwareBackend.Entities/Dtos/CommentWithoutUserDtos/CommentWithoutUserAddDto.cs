using System;
namespace CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos
{
    public class CommentWithoutUserAddDto
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public sbyte Star { get; set; }
        public int ArticleId { get; set; }
    }
}

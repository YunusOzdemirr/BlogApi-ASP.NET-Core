using System;
namespace CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos
{
    public class ArticlePictureUpdateDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public byte[] File { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}

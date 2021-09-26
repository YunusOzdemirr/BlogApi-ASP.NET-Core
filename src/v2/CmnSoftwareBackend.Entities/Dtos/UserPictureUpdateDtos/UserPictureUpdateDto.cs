using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CmnSoftwareBackend.Entities.Dtos.UserPictureUpdateDtos
{
    public class UserPictureUpdateDto
    {
        [DisplayName("Kullanıcının Resim Kodu")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public Guid Id { get; set; }

        [DisplayName("Kullanıcı Kodu")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public Guid UserId { get; set; }

        [DisplayName("Resim Dosyası")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Base64File { get; set; }
    }
}

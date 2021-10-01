using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CmnSoftwareBackend.Entities.Dtos.UserPictureDtos
{
    public class UserPictureAddDto
    {
        [DisplayName("Kullanıcı Kodu")]
        [Required(ErrorMessage ="{0} alanı boş geçilemez")]
        public Guid UserId{ get; set; }

        [DisplayName("Resim Dosyası")]
        [Required(ErrorMessage ="{0} alanı boş geçilemez")]
        public string Base64File { get; set; }
    }
}

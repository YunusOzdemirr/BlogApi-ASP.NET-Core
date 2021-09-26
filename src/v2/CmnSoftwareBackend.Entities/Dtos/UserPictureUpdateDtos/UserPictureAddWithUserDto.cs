using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CmnSoftwareBackend.Entities.Dtos.UserPictureUpdateDtos
{
    public class UserPictureAddWithUserDto
    {
        [DisplayName("Resim Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(250, ErrorMessage = "{0} alanı en az {1} en fazla karakter uzunluğunda olmalıdır.")]
        public string Name { get; set; }
        [DisplayName("Resim Dosyası")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string File { get; set; }
    }
}

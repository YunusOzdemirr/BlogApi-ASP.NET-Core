using System;
namespace CmnSoftwareBackend.Entities.Dtos.EmailDtos
{
    public class EmailSendDto
    {
        public string Subject{ get; set; }
        public string Content { get; set; }
        public string EmailAdress { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using CmnSoftwareBackend.Entities.Concrete;
using CmnSoftwareBackend.Entities.Dtos.EmailDtos;
using CmnSoftwareBackend.Entities.Dtos.LandingEmailDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Services.Utilities;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CmnSoftwareBackend.Services.Concrete
{
    public class MailManager:IMailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly BodyBuilder _bodyBuilder;

        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _bodyBuilder = new BodyBuilder();
        }

        public IResult SendEmaiL(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.SenderEmail);
            message.To.Add(new MailAddress(emailSendDto.EmailAdress));
            message.Subject = emailSendDto.Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = emailSendDto.Content;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = _smtpSettings.Port;
            smtp.Host = _smtpSettings.Server;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendAsync(message, "SendEmaiL");

            return new Result(ResultStatus.Success, "mesaj gönderildi");
        }


        public IResult SendLandingEmail(LandingEmailDto landingEmailDto)
        {
            if (landingEmailDto.Password != "rock,paper,scissors")
                throw new NotFoundArgumentException(Messages.General.ValidationError(),new Error("Lütfen geçerli bir şifre giriniz.","Password"));

            //return new Result(ResultStatus.Warning, $"Bir veya daha fazla validasyon hatası ile karşılaşıldı.", new List<Error> { new Error { PropertyName = "Password", Message = $"Lütfen geçerli bir şifre girdikten sonra tekrar deneyiniz." } });

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.SenderEmail);
            message.To.Add(new MailAddress("yunusozdemir468@hotmail.com"));
            message.Subject = landingEmailDto.Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = $"{landingEmailDto.Message} sent by {landingEmailDto.Email}";
            SmtpClient smtp = new SmtpClient();
            smtp.Port = _smtpSettings.Port;
            smtp.Host = _smtpSettings.Server;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.SendAsync(message, "SendEmaiL");

            return new Result(ResultStatus.Success, "mesaj gönderildi");
        }
    }
}
using System;
using CmnSoftwareBackend.Entities.Dtos.EmailDtos;
using CmnSoftwareBackend.Entities.Dtos.LandingEmailDtos;
using CmnSoftwareBackend.Shared.Utilities.Results.Abstract;

namespace CmnSoftwareBackend.Services.Abstract
{
    public interface IMailService
    {
        IResult SendEmaiL(EmailSendDto emailSendDto);
        IResult SendLandingEmail(LandingEmailDto landingEmailDto);
    }
}

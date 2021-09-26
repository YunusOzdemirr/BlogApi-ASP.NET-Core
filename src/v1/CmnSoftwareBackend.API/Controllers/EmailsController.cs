using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.EmailDtos;
using CmnSoftwareBackend.Entities.Dtos.LandingEmailDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IMailService _mailService;
        public EmailsController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public IActionResult SendEmail(EmailSendDto emailSendDto)
        {
            var emailSentResult = _mailService.SendEmaiL(emailSendDto);
            switch (emailSentResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = emailSentResult.ResultStatus,
                        Data = null,
                        Detail = emailSentResult.Message,
                        Message = emailSentResult.Message,
                        Href = Url.Link("", new { Controller = "Emails", Action = "SendEmail" }),
                        ValidationErrors = emailSentResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = emailSentResult.ResultStatus,
                        Data = null,
                        Detail = emailSentResult.Message,
                        Message = emailSentResult.Message,
                        Href = Url.Link("", new { Controller = "Emails", Action = "SendEmail" }),
                        ValidationErrors = emailSentResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public IActionResult SendLandingEmail(LandingEmailDto landingEmailDto)
        {
            var emailSentResult = _mailService.SendLandingEmail(landingEmailDto);
            switch (emailSentResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = emailSentResult.ResultStatus,
                        Data = null,
                        Detail = emailSentResult.Message,
                        Message = emailSentResult.Message,
                        Href = Url.Link("", new { Controller = "Emails", Action = "SendLandingEmail" }),
                        ValidationErrors = emailSentResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = emailSentResult.ResultStatus,
                        Data = null,
                        Detail = emailSentResult.Message,
                        Message = emailSentResult.Message,
                        Href = Url.Link("", new { Controller = "Emails", Action = "SendLandingEmail" }),
                        ValidationErrors = emailSentResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
    }
}

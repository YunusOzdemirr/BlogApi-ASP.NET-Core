using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersAuthController(IUserAuthService userAuthService, IHttpContextAccessor httpContextAccessor)
        {
            _userAuthService = userAuthService;
            _httpContextAccessor = httpContextAccessor;
        }

        //REGISTER tested
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var registerResult = await _userAuthService.RegisterAsync(userRegisterDto);
            switch (registerResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = registerResult.ResultStatus,
                        Data = registerResult.Data,
                        Detail = registerResult.Message,
                        Message = registerResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "Register" }),
                        ValidationErrors = registerResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = registerResult.ResultStatus,
                        Data = registerResult.Data,
                        Message = registerResult.Message,
                        Detail = registerResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "Register" }),
                        ValidationErrors = registerResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        //LOGIN tested
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var loginResult = await _userAuthService.LoginAsync(userLoginDto);
            switch (loginResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = loginResult.ResultStatus,
                        Data = loginResult.Data,
                        Detail = loginResult.Message,
                        Message = loginResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "Login" }),
                        ValidationErrors = loginResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = loginResult.ResultStatus,
                        Data = loginResult.Data,
                        Message = loginResult.Message,
                        Detail = loginResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "Login" }),
                        ValidationErrors = loginResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        //ACTIVATE EMAIL tested
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> ActivateEmail(UserEmailActivateDto userEmailActivateDto)
        {
            var activateEmailResult = await _userAuthService.ActiveEmailByActivationCodeAsync(userEmailActivateDto);
            switch (activateEmailResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = activateEmailResult.ResultStatus,
                        Data = activateEmailResult.Data,
                        Detail = activateEmailResult.Message,
                        Message = activateEmailResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ActivateEmail" }),
                        ValidationErrors = activateEmailResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = activateEmailResult.ResultStatus,
                        Data = activateEmailResult.Data,
                        Message = activateEmailResult.Message,
                        Detail = activateEmailResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ActivateEmail" }),
                        ValidationErrors = activateEmailResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        //RESEND ACTIVATION CODE tested
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> ReSendActivationCode(string emailAddress)
        {

            var reSendActivationCodeResult = await _userAuthService.ReSendActivationCodeAsync(emailAddress);
            switch (reSendActivationCodeResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = reSendActivationCodeResult.ResultStatus,
                        Data = reSendActivationCodeResult.Data,
                        Detail = reSendActivationCodeResult.Message,
                        Message = reSendActivationCodeResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ReSendActivationCode" }),
                        ValidationErrors = reSendActivationCodeResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = reSendActivationCodeResult.ResultStatus,
                        Data = reSendActivationCodeResult.Data,
                        Message = reSendActivationCodeResult.Message,
                        Detail = reSendActivationCodeResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ReSendActivationCode" }),
                        ValidationErrors = reSendActivationCodeResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        //FORGOT PASSWORD tested
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> ForgotPassword(string emailAddress)
        {

            var forgotPasswordResult = await _userAuthService.ForgotPasswordAsync(emailAddress);
            switch (forgotPasswordResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = forgotPasswordResult.ResultStatus,
                        Data = forgotPasswordResult.Data,
                        Detail = forgotPasswordResult.Message,
                        Message = forgotPasswordResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ForgotPassword" }),
                        ValidationErrors = forgotPasswordResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = forgotPasswordResult.ResultStatus,
                        Data = forgotPasswordResult.Data,
                        Message = forgotPasswordResult.Message,
                        Detail = forgotPasswordResult.Message,
                        Href = Url.Link("", new { Controller = "UsersAuth", Action = "ForgotPassword" }),
                        ValidationErrors = forgotPasswordResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

    }
}
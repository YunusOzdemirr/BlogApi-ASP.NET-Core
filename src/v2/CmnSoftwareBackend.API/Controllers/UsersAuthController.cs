using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
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
        [Route("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.IpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var registerResult = await _userAuthService.RegisterAsync(userRegisterDto);
            return Ok(new SuccessDataApiResult(registerResult, Url.Link("", new { Controller = "UsersAuth", Action = "Register" })));
        }

        //LOGIN tested
        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var loginResult = await _userAuthService.LoginAsync(userLoginDto);
            return Ok(new SuccessDataApiResult(loginResult, Url.Link("", new { Controller = "UsersAuth", Action = "Login" })));
        }

        //ACTIVATE EMAIL tested
        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> ActivateEmail(UserEmailActivateDto userEmailActivateDto)
        {
            var activateEmailResult = await _userAuthService.ActiveEmailByActivationCodeAsync(userEmailActivateDto);
            return Ok(new SuccessDataApiResult(activateEmailResult, Url.Link("", new { Controller = "UsersAuth", Action = "ActivateEmail" })));
        }

        //RESEND ACTIVATION CODE tested
        [HttpPost]
        [ProducesResponseType(typeof(SuccessDataApiResult), StatusCodes.Status200OK)]
        [Route("[action]")]
        public async Task<IActionResult> ReSendActivationCode(string emailAddress)
        {

            var reSendActivationCodeResult = await _userAuthService.ReSendActivationCodeAsync(emailAddress);
            return Ok(new SuccessDataApiResult(reSendActivationCodeResult, Url.Link("", new { Controller = "UsersAuth", Action = "ReSendActivationCode" })));
        }

        //FORGOT PASSWORD tested
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ForgotPassword(string emailAddress)
        {

            var forgotPasswordResult = await _userAuthService.ForgotPasswordAsync(emailAddress);
            return Ok(new SuccessDataApiResult(forgotPasswordResult, Url.Link("", new { Controller = "UsersAuth", Action = "ForgotPassword" })));
        }

    }
}
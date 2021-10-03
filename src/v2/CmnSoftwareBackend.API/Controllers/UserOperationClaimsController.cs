using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserOperationClaimsController : Controller
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPost]
       
        [Route("[action]")]
        public async Task<IActionResult> GetAllByUserIdAsync(Guid userId)
        {
            var getAllByUserIdResult = await _userOperationClaimService.GetAllByUserId(userId);
            return Ok(new SuccessDataApiResult(getAllByUserIdResult, Url.Link("", new { Controller = "UserOperationClaims", Action = "GetAllByUserIdAsync" })));
            
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto)
        {
            var addResult = await _userOperationClaimService.AddAsync(userOperationClaimAddDto);
            return Ok(new SuccessDataApiResult(addResult, Url.Link("", new { Controller = "UserOperationClaims", Action = "AddAsync" })));
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(UserOperationClaimUpdateDto userOperationClaimUpdateDto)
        {
            var updateResult = await _userOperationClaimService.UpdateAsync(userOperationClaimUpdateDto);
            return Ok(new SuccessDataApiResult(updateResult, Url.Link("", new { Controller = "UserOperationClaims", Action = "UpdateAsync" })));

        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(UserOperationClaimDeleteDto userOperationClaimDeleteDto)
        {
            var deleteResult = await _userOperationClaimService.DeleteAsync(userOperationClaimDeleteDto);
            return Ok(new SuccessApiResult(deleteResult, Url.Link("", new { Controller = "UserOperationClaims", Action = "DeleteAsync" })));

        }
    }
}

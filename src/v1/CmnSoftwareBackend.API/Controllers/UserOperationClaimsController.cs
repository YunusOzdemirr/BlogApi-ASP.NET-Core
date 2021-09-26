using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.UserOperationClaimDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : Controller
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> GetAllByUserIdAsync(Guid userId)
        {
            var getAllByUserIdResult = await _userOperationClaimService.GetAllByUserId(userId);
            switch (getAllByUserIdResult.ResultStatus) //ResultStatus
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = getAllByUserIdResult.ResultStatus,
                        Data = getAllByUserIdResult.Data,
                        Detail = getAllByUserIdResult.Message,
                        Message = getAllByUserIdResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "GetAllByUserId" }),
                        ValidationErrors = getAllByUserIdResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = getAllByUserIdResult.ResultStatus,
                        Data = getAllByUserIdResult.Data,
                        Message = getAllByUserIdResult.Message,
                        Detail = getAllByUserIdResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "GetAllByUserId" }),
                        ValidationErrors = getAllByUserIdResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(UserOperationClaimAddDto userOperationClaimAddDto)
        {
            var addResult = await _userOperationClaimService.AddAsync(userOperationClaimAddDto);
            switch (addResult.ResultStatus) //ResultStatus
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = addResult.ResultStatus,
                        Data = addResult.Data,
                        Detail = addResult.Message,
                        Message = addResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Add" }),
                        ValidationErrors = addResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = addResult.ResultStatus,
                        Data = addResult.Data,
                        Message = addResult.Message,
                        Detail = addResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Add" }),
                        ValidationErrors = addResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(UserOperationClaimUpdateDto userOperationClaimUpdateDto)
        {
            var updateResult = await _userOperationClaimService.UpdateAsync(userOperationClaimUpdateDto);
            switch (updateResult.ResultStatus) //ResultStatus
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.Data,
                        Detail = updateResult.Message,
                        Message = updateResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Update" }),
                        ValidationErrors = updateResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.Data,
                        Message = updateResult.Message,
                        Detail = updateResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Update" }),
                        ValidationErrors = updateResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(UserOperationClaimDeleteDto userOperationClaimDeleteDto)
        {
            var deleteResult = await _userOperationClaimService.DeleteAsync(userOperationClaimDeleteDto);
            switch (deleteResult.ResultStatus) //ResultStatus
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = null,
                        Detail = deleteResult.Message,
                        Message = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Delete" }),
                        ValidationErrors = deleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest,
                    });
                default: //ResultStatus.Success
                    return Ok(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = null,
                        Message = deleteResult.Message,
                        Detail = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "UserOperationClaims", Action = "Delete" }),
                        ValidationErrors = deleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
    }
}

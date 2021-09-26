using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : Controller
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(bool isActive = true, bool isDeleted = false, bool isAscending = false)
        {
            var getAllResult = await _operationClaimService.GetAllAsync(isActive, isDeleted, isAscending);
            switch (getAllResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = getAllResult.ResultStatus,
                        Data = getAllResult.Data,
                        Detail = getAllResult.Message,
                        Message = getAllResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" }),
                        ValidationErrors = getAllResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = getAllResult.ResultStatus,
                        Data = getAllResult.Data,
                        Detail = getAllResult.Message,
                        Message = getAllResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" }),
                        ValidationErrors = getAllResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int operationClaimId)
        {
            var getResult = await _operationClaimService.GetByIdAsync(operationClaimId);
            switch (getResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus=getResult.ResultStatus,
                        Data=getResult.Data,
                        Detail=getResult.Message,
                        Message=getResult.Message,
                        Href=Url.Link("",new {Controller="OperationClaims",Action="GetById" }),
                        ValidationErrors=getResult.ValidationErrors,
                        StatusCode=HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus=getResult.ResultStatus,
                        Data=getResult.Data,
                        Detail=getResult.Message,
                        Message=getResult.Message,
                        Href=Url.Link("",new { Controller="OperationClaims", Action="GetById"})
                    });
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> Add(OperationClaimAddDto operationClaimAddDto)
        {
            var addResult = await _operationClaimService.AddAsync(operationClaimAddDto);
            switch (addResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus=addResult.ResultStatus,
                        Data=addResult.Data,
                        Detail=addResult.Message,
                        Message=addResult.Message,
                        Href=Url.Link("",new { Controller="OperationClaims",Action="Add"}),
                        ValidationErrors=addResult.ValidationErrors,
                        StatusCode=HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus=addResult.ResultStatus,
                        Data=addResult.Data,
                        Detail=addResult.Message,
                        Message=addResult.Message,
                        Href=Url.Link("", new { Controller="OperationClaims",Action="Add"}),
                        ValidationErrors=addResult.ValidationErrors,
                        StatusCode=HttpStatusCode.OK
                    });
            }
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> Update(OperationClaimUpdateDto operationClaimUpdateDto)
        {
            var updateResult = await _operationClaimService.UpdateAsync(operationClaimUpdateDto);
            switch (updateResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.Data,
                        Detail = updateResult.Message,
                        Message = updateResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "Update" }),
                        ValidationErrors = updateResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.Data,
                        Detail = updateResult.Message,
                        Message = updateResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "Update" }),
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
        public async Task<IActionResult> Delete(int operationClaimId)
        {
            var deleteResult = await _operationClaimService.DeleteAsync(operationClaimId);
            switch (deleteResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = deleteResult.Data,
                        Detail = deleteResult.Message,
                        Message = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "Delete" }),
                        ValidationErrors = deleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = deleteResult.Data,
                        Detail = deleteResult.Message,
                        Message = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "Delete" }),
                        ValidationErrors = deleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> HardDelete(int operationClaimId)
        {
            var hardDeleteResult = await _operationClaimService.DeleteAsync(operationClaimId);
            switch (hardDeleteResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = hardDeleteResult.ResultStatus,
                        Data = hardDeleteResult.Data,
                        Detail = hardDeleteResult.Message,
                        Message = hardDeleteResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "HardDelete" }),
                        ValidationErrors = hardDeleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = hardDeleteResult.ResultStatus,
                        Data = hardDeleteResult.Data,
                        Detail = hardDeleteResult.Message,
                        Message = hardDeleteResult.Message,
                        Href = Url.Link("", new { Controller = "OperationClaims", Action = "HardDelete" }),
                        ValidationErrors = hardDeleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }



    }
}

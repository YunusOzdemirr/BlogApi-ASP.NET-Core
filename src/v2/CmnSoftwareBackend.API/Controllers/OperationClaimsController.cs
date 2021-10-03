using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.OperationClaimDtos;
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
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(200)]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class OperationClaimsController : Controller
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll(bool isActive = true, bool isDeleted = false, bool isAscending = false)
        {
            var getAllResult = await _operationClaimService.GetAllAsync(isActive, isDeleted, isAscending);
            return Ok(new SuccessDataApiResult(getAllResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int operationClaimId)
        {
            var getResult = await _operationClaimService.GetByIdAsync(operationClaimId);
            return Ok(new SuccessDataApiResult(getResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Add(OperationClaimAddDto operationClaimAddDto)
        {
            var addResult = await _operationClaimService.AddAsync(operationClaimAddDto);
            return Ok(new SuccessDataApiResult(addResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update(OperationClaimUpdateDto operationClaimUpdateDto)
        {
            var updateResult = await _operationClaimService.UpdateAsync(operationClaimUpdateDto);
            return Ok(new SuccessDataApiResult(updateResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Delete(int operationClaimId)
        {
            var deleteResult = await _operationClaimService.DeleteAsync(operationClaimId);
            return Ok(new SuccessDataApiResult(deleteResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> HardDelete(int operationClaimId)
        {
            var hardDeleteResult = await _operationClaimService.DeleteAsync(operationClaimId);
            return Ok(new SuccessDataApiResult(hardDeleteResult, Url.Link("", new { Controller = "OperationClaims", Action = "GetAll" })));
        }
    }
}

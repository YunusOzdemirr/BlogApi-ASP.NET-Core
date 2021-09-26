using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Entities.Concrete;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending = false, int currentPage = 1, int pageSize = 20, OrderBy orderBy = OrderBy.CreatedDate)
        {
            var getAllResult = await _categoryService.GetAllAsync(isActive, isDeleted, isAscending, currentPage, pageSize, orderBy);
            return Ok(new ApiResult
            {
                ResultStatus = getAllResult.ResultStatus,
                Data = getAllResult.Data,
                Message = getAllResult.Message,
                Detail = getAllResult.Message,
                Href = Url.Link("", new { Controller = "Categories", Action = "GetAll" }),
                ValidationErrors = getAllResult.ValidationErrors,
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int categoryId)
        {
            var getByIdResult = await _categoryService.GetByIdAsync(categoryId);
            switch (getByIdResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = getByIdResult.ResultStatus,
                        Data = getByIdResult.Data,
                        Detail = getByIdResult.Message,
                        Message = getByIdResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "GetById" }),
                        ValidationErrors = getByIdResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = getByIdResult.ResultStatus,
                        Data = getByIdResult.Data,
                        Detail = getByIdResult.Message,
                        Message = getByIdResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "GetById" }),
                        ValidationErrors = getByIdResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(CategoryAddDto categoryAddDto)
        {
            var addResult = await _categoryService.AddAsync(categoryAddDto);
            switch (addResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = addResult.ResultStatus,
                        Data = addResult.Data,
                        Message = addResult.Message,
                        Detail = addResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "AddAsync" }),
                        ValidationErrors = addResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = addResult.ResultStatus,
                        Data = addResult.Data,
                        Message = addResult.Message,
                        Detail = addResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "AddAsync" }),
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
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updateResult = await _categoryService.UpdateAsync(categoryUpdateDto);
            switch (updateResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.ResultStatus,
                        Message = updateResult.Message,
                        Detail = updateResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "UpdateAsync" }),
                        ValidationErrors = updateResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = updateResult.ResultStatus,
                        Data = updateResult.Data,
                        Message = updateResult.Message,
                        Detail = updateResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "UpdateAsync" }),
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
        public async Task<IActionResult> DeleteAsync(int categoryId)
        {
            var deleteResult = await _categoryService.DeleteAsync(categoryId);
            switch (deleteResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = deleteResult.Data,
                        Message = deleteResult.Message,
                        Detail = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "DeleteAsync" }),
                        ValidationErrors = deleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = deleteResult.ResultStatus,
                        Data = deleteResult.Data,
                        Message = deleteResult.Message,
                        Detail = deleteResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "DeleteAsync" }),
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
        public async Task<IActionResult> HardDeleteAsync(int categoryId)
        {
            var hardDeleteResult = await _categoryService.HardDeleteAsync(categoryId);
            switch (hardDeleteResult.ResultStatus)
            {
                case ResultStatus.Warning:
                    return BadRequest(new ApiResult
                    {
                        ResultStatus = hardDeleteResult.ResultStatus,
                        Data = null,
                        Detail = hardDeleteResult.Message,
                        Message = hardDeleteResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "HardDeleteAsync" }),
                        ValidationErrors = hardDeleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.BadRequest
                    });
                default:
                    return Ok(new ApiResult
                    {
                        ResultStatus = hardDeleteResult.ResultStatus,
                        Data = null,
                        Detail = hardDeleteResult.Message,
                        Message = hardDeleteResult.Message,
                        Href = Url.Link("", new { Controller = "Categories", Action = "HardDeleteAsync" }),
                        ValidationErrors = hardDeleteResult.ValidationErrors,
                        StatusCode = HttpStatusCode.OK
                    });
            }
        }
    }
}

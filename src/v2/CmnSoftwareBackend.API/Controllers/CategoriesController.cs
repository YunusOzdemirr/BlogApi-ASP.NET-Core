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
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
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
            return Ok(new SuccessDataApiResult(getAllResult, Url.Link("", new { Controller = "Categories", Action = "GetAll" })));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        [Authorize(Roles = "NormalUser")]
        public async Task<IActionResult> GetByIdAsync(int categoryId)
        {
            var getByIdResult = await _categoryService.GetByIdAsync(categoryId);
            return Ok(new SuccessDataApiResult(getByIdResult, Url.Link("", new { Controller = "Categories", Action = "GetByIdAsync" })));

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(CategoryAddDto categoryAddDto)
        {
            var addResult = await _categoryService.AddAsync(categoryAddDto);
            return Ok(new SuccessDataApiResult(addResult, Url.Link("", new { Controller = "Categories", Action = "AddAsync" })));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updateResult = await _categoryService.UpdateAsync(categoryUpdateDto);
            return Ok(new SuccessDataApiResult(updateResult, Url.Link("", new { Controller = "Categories", Action = "UpdateAsync" })));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(int categoryId)
        {
            var deleteResult = await _categoryService.DeleteAsync(categoryId);
            return Ok(new SuccessDataApiResult(deleteResult, Url.Link("", new { Controller = "Categories", Action = "DeleteAsync" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(int categoryId)
        {
            var hardDeleteResult = await _categoryService.HardDeleteAsync(categoryId);
            return Ok(new  SuccessApiResult(hardDeleteResult, Url.Link("", new { Controller = "Categories", Action = "HardDeleteAsync" })));
           
        }
    }
}

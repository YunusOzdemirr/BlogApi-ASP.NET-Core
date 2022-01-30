using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.CategoryDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Extensions;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using FrontEnd.Mvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync(isActive: true, isDeleted: false, isAscending: true, currentPage: 1, pageSize: 20, orderBy: 0);
            if (result.ResultStatus == ResultStatus.Success)
                return View(result.Data);

            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryAddDto dto)
        {
            var ajaxViewModel = new CategoryAddAjaxViewModel() { CategoryAddDto = dto, CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", dto) };
            var result = await _categoryService.AddAsync(dto);
            return View(result.Data);
        }
    }
}
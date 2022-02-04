using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync(isActive: true, isDeleted: false, isAscending: true, currentPage: 1, pageSize: 200, orderBy: 0);
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
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            //var ajaxViewModel = new CategoryAddAjaxViewModel() { CategoryAddDto = dto, CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", dto) };
            //var result = await _categoryService.AddAsync(dto);
            //return View(result.Data);
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(categoryAddDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryDto = _mapper.Map<CategoryDto>(result.Data);
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel()
                    {
                        CategoryAddDto = categoryAddDto,
                        CategoryDto = categoryDto,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryDto)
                    });
                    return Json(categoryAddAjaxModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel()
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto),
            });
            return Json(categoryAddAjaxErrorModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
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
    }
}
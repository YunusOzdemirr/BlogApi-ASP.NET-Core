using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryAndArticlesController : Controller
    {
        private readonly ICategoryAndArticleService _categoryAndArticleService;

        public CategoryAndArticlesController(ICategoryAndArticleService categoryAndArticleService)
        {
            _categoryAndArticleService = categoryAndArticleService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(CategoryAndArticleAddDto categoryAndArticleAddDto)
        {
            var result = await _categoryAndArticleService.AddAsync(categoryAndArticleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "Add" })));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto)
        {
            var result = await _categoryAndArticleService.HardDeleteAsync(categoryAndArticlesUpdateDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "HardDelete" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool isAscending,int currentPage,int pageSize,OrderBy orderBy,bool includeArticle,bool includeCategory)
        {
            var result = await _categoryAndArticleService.GetAllAsync(isAscending,currentPage,pageSize,orderBy,includeArticle,includeCategory);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "GetAll" })));
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(CategoryAndArticlesUpdateDto categoryAndArticlesUpdateDto)
        {
            var result = await _categoryAndArticleService.UpdateAsync(categoryAndArticlesUpdateDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "Update" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByCategoryId(int categoryId)
        {
            var result = await _categoryAndArticleService.GetArticleByCategoryId(categoryId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "GetArticleByCategoryId" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCategoryByArticleId(int articleId)
        {
            var result = await _categoryAndArticleService.GetCategoryByArticleId(articleId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CategoryAndArticles", Action = "GetCategoryByArticleId" })));
        }



    }
}


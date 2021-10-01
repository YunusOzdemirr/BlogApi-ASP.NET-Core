using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.Dtos.CategoryAndArticleDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ApiController]
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


    }
}


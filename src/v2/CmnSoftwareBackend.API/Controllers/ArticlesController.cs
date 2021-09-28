using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [ProducesResponseType(200)]

        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticlePicture)
        {
            var result = await _articleService.GetAllAsync(isActive, isDeleted, isAscending, currentPage, pageSize, orderBy, includeArticlePicture);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetAll" })));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int articleId, bool includeArticlePicture)
        {
            var result = await _articleService.GetByIdAsync(articleId, includeArticlePicture);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetById" })));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByUserId(Guid userId)
        {
            var article = await _articleService.GetArticleByUserId(userId);
            return Ok(new SuccessDataApiResult(article, Url.Link("", new { Controller = "Articles", Action = "GetArticleByUserId" })));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByArticlePictureId(int ArticlePictureId)
        {
            var articles =await _articleService.GetArticleByArticlePictureId(ArticlePictureId);
            return Ok(new SuccessDataApiResult(articles,Url.Link("",new {Controller="Articles",Action="GetArticleByArticlePictureId" })));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(ArticleAddDto articleAddDto)
        {
            var result = await _articleService.AddAsync(articleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Add" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(ArticleAddDto articleAddDto)
        {
            var result = await _articleService.AddAsync(articleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Delete" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(ArticleAddDto articleAddDto)
        {
            var result = await _articleService.AddAsync(articleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Update" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(ArticleAddDto articleAddDto)
        {
            var result = await _articleService.AddAsync(articleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "HardDelete" })));
        }

    }
}


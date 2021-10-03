using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticleDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [Authorize]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticlePicture, bool includeCommentWithoutUser, bool includeCommentWithUser)
        {
            var result = await _articleService.GetAllAsync(isActive, isDeleted, isAscending, currentPage, pageSize, orderBy, includeArticlePicture, includeCommentWithoutUser, includeCommentWithUser);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetAll" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int articleId, bool includeArticlePicture, bool commentWithUser, bool commentWithoutUser)
        {
            var result = await _articleService.GetByIdAsync(articleId, includeArticlePicture, commentWithUser, commentWithoutUser);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetById" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByCommentWithoutUserId(int commentWithoutUserId)
        {
            var result = await _articleService.GetArticleByCommentWithoutUserIdAsync(commentWithoutUserId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetArticleByCommentWithoutUserId" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByCommentWithUserId(int commentWithUserId)
        {
            var result = await _articleService.GetArticleByCommentWithUserIdAsync(commentWithUserId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "GetArticleByCommentWithUserId" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByUserId(Guid userId)
        {
            var article = await _articleService.GetArticleByUserId(userId);
            return Ok(new SuccessDataApiResult(article, Url.Link("", new { Controller = "Articles", Action = "GetArticleByUserId" })));
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetArticleByArticlePictureId(int ArticlePictureId)
        {
            var articles = await _articleService.GetArticleByArticlePictureId(ArticlePictureId);
            return Ok(new SuccessDataApiResult(articles, Url.Link("", new { Controller = "Articles", Action = "GetArticleByArticlePictureId" })));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(ArticleAddDto articleAddDto)
        {
            var result = await _articleService.AddAsync(articleAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Add" })));
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(int articleId, Guid CreatedUserId)
        {
            var result = await _articleService.DeleteAsync(articleId, CreatedUserId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Delete" })));
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(ArticleUpdateDto articleUpdateDto)
        {
            var result = await _articleService.UpdateAsync(articleUpdateDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "Articles", Action = "Update" })));
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> HardDeleteAsync(int articleId)
        {
            var result = await _articleService.HardDeleteAsync(articleId);
            return Ok(new SuccessApiResult(result, Url.Link("", new { Controller = "Articles", Action = "HardDelete" })));
        }

    }
}


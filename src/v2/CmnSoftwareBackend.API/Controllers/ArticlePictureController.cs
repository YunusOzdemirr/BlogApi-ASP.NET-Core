using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.ArticlePictureDtos;
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
    public class ArticlePictureController : Controller
    {
        private readonly IArticlePictureService _articlePictureService;

        public ArticlePictureController(IArticlePictureService articlePictureService)
        {
            _articlePictureService = articlePictureService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetById(int articlePictureId, bool includeArticle)
        {
            var getById = await _articlePictureService.GetByIdAsync(articlePictureId, includeArticle);
            return Ok(new SuccessDataApiResult(getById, Url.Link("", new { Controller = "ArticlePictures", Action = "GetById" })));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetArticlePictureByArticleId(int articleId)
        {
            var article = await _articlePictureService.GetArticlePictureByArticleId(articleId);
            return Ok(new SuccessDataApiResult(article, Url.Link("", new { Controller = "ArticlePictures", Action = "GetArticlePictureByArticleId" })));
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle)
        {
            var getAll = await _articlePictureService.GetAllAsync(isActive, isActive, isAscending, currentPage, pageSize, orderBy, includeArticle);
            return Ok(new SuccessDataApiResult(getAll, Url.Link("", new { Controller = "ArticlePictures", Action = "GetAll" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
       
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(ArticlePictureAddDto articlePictureAddDto)
        {
            var addResult = await _articlePictureService.AddAsync(articlePictureAddDto);
            return Ok(new SuccessDataApiResult(addResult, Url.Link("", new { Controller = "ArticlePictures", Action = "Add" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(int articlePictureId, Guid CreatedByUserId)
        {
            var deleteResult = await _articlePictureService.DeleteAsync(articlePictureId, CreatedByUserId);
            return Ok(new SuccessDataApiResult(deleteResult, Url.Link("", new { Controller = "ArticlePictures", Action = "Delete" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(ArticlePictureUpdateDto articlePictureUpdateDto)
        {
            var updateResult = await _articlePictureService.UpdateAsync(articlePictureUpdateDto);
            return Ok(new SuccessDataApiResult(updateResult, Url.Link("", new { Controller = "ArticlePictures", Action = "Update" })));
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(int articlePictureId)
        {
            var hardDeleteResult = await _articlePictureService.HardDeleteAsync(articlePictureId);
            return Ok(new SuccessApiResult(hardDeleteResult, Url.Link("", new { Controller = "ArticlePictures", Action = "HardDelete" })));
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithUserDtos;
using CmnSoftwareBackend.Services.Abstract;
using CmnSoftwareBackend.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmnSoftwareBackend.API.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Route("api/[controller]")]
    public class CommentWithUsersController : Controller
    {
        private readonly ICommentWithUserService _commentWithUserService;

        public CommentWithUsersController(ICommentWithUserService commentWithUserService)
        {
            _commentWithUserService = commentWithUserService;
        }
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle, bool includeUser)
        {
            var result = await _commentWithUserService.GetAllAsync(isActive, isDeleted, isAscending, currentPage, pageSize, orderBy, includeArticle, includeUser);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "GetAll" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int commentWithUserId, bool includeArticle)
        {
            var result = await _commentWithUserService.GetByIdAsync(commentWithUserId, includeArticle);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "GetById" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(CommentWithUserAddDto commentWithUserAddDto)
        {
            var result = await _commentWithUserService.AddAsync(commentWithUserAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "Add" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(CommentWithUserUpdateDto commentWithUserUpdateDto)
        {
            var result = await _commentWithUserService.UpdateAsync(commentWithUserUpdateDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "Update" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> GetCommentByUserId(Guid userId, bool includeArticle)
        {
            var result =await _commentWithUserService.GetAllCommentByUserId(userId, includeArticle);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "GetCommentByUserId" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(int commentWithUserId, Guid CreatedByUserId)
        {
            var result = await _commentWithUserService.DeleteAsync(commentWithUserId, CreatedByUserId);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "GetCommentByUserId" })));
        }

        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(int commentId)
        {
            var result = await _commentWithUserService.HardDeleteAsync(commentId);
            return Ok(new SuccessApiResult(result, Url.Link("", new { Controller = "CommentWithUser", Action = "GetCommentByUserId" })));
        }


    }
}


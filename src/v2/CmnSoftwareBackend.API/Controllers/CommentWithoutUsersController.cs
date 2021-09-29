using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Entities.Dtos.CommentWithoutUserDtos;
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
    public class CommentWithoutUsersController : Controller
    {
        private readonly ICommentWithoutUserService _commentWithoutUserService;

        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy, bool includeArticle)
        {
            var result = await _commentWithoutUserService.GetAllAsync(isActive, isDeleted, isAscending, currentPage, pageSize, orderBy, includeArticle);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithoutUser", Action = "GetAll" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int commentWithoutUserId, bool includeArticle)
        {
            var result = await _commentWithoutUserService.GetByIdAsync(commentWithoutUserId, includeArticle);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithoutUser", Action = "GetById" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(CommentWithoutUserAddDto commentWithoutUserAddDto)
        {
            var result = await _commentWithoutUserService.AddAsync(commentWithoutUserAddDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithoutUser", Action = "Add" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(int commentWithoutUserId)
        {
            var result = await _commentWithoutUserService.DeleteAsync(commentWithoutUserId);
            return Ok(new SuccessDataApiResult(result,Url.Link("",new {Controller="CommentWithotUser",Action="Delete" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> HardDeleteAsync(int commentWithoutUserId)
        {
            var result = await _commentWithoutUserService.HardDeleteAsync(commentWithoutUserId);
            return Ok(new SuccessApiResult(result, Url.Link("", new { Controller = "CommentWithoutUser", Action = "HardDelete" })));
        }
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(CommentWithoutUserUpdateDto commentWithoutUserUpdateDto)
        {
            var result = await _commentWithoutUserService.UpdateAsync(commentWithoutUserUpdateDto);
            return Ok(new SuccessDataApiResult(result, Url.Link("", new { Controller = "CommentWithoutUser", Action = "UpdateAsync" })));
        }

    }
}


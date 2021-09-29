using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmnSoftwareBackend.Entities.ComplexTypes;
using CmnSoftwareBackend.Services.Abstract;
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
        public async Task<IActionResult> GetAllAsync(bool? isActive, bool? isDeleted, bool isAscending,int currentPage,int pageSize,OrderBy orderBy,bool include Article)
        {

        }
    }
}


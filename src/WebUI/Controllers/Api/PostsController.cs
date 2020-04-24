using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Api
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            return Ok(await _postsRepository.GetAllAsync());
        }
    }
}
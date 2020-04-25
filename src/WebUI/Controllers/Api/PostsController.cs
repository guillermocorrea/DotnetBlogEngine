using Application.Common.Models;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPostsByStatus;
using Application.Posts.Queries.GetPostDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Controllers.Api
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PostDto>>> Get([FromQuery] string status)
        {
            return Ok(await _mediator.Send(new GetAllPostsByStatusQuery { Status = status }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDto>> Get(int id)
        {
            var query = new GetPostDetailsQuery { PostId = id };
            var post = await _mediator.Send(query);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Editor",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Approve(int id)
        {
            var command = new ApprovePostCommand { PostId = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}/reject")]
        [Authorize(Roles = "Editor",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Reject(int id)
        {
            var command = new RejectPostCommand { PostId = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
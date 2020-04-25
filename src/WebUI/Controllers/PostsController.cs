using Application.Comments.Commands.CreateComment;
using Application.Common.Models;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetAllPostsByStatus;
using Application.Posts.Queries.GetPostDetails;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using static Domain.Post;

namespace WebUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string status)
        {
            var query = new GetAllPostsByStatusQuery { Status = status };
            var result = await _mediator.Send(query);
            return View(result);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new GetPostDetailsQuery { PostId = id.Value };
            var post = await _mediator.Send(query);
            return post == null ? NotFound() : (IActionResult)View(new PostDetailsViewModel
            {
                Post = post,
                NewComment = new CreateCommentDto()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto newComment)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = newComment.PostId });
            }

            var query = new CreateCommentCommand { NewComment = newComment };
            await _mediator.Send(query);
            return RedirectToAction(nameof(Details), new { id = newComment.PostId });
        }

        // GET: Posts/Create
        [Authorize(Roles = Roles.Writer)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [Authorize(Roles = Roles.Writer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Status,UserId")] PostDto post)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreatePostCommand { Post = post });
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Authorize(Roles = "Editor,Writer")]
        [HttpPost]
        public async Task<IActionResult> SubmitPost(int id)
        {
            var command = new SubmitPostCommand { PostId = id };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> ApprovePost(int id)
        {
            var command = new ApprovePostCommand { PostId = id };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> RejectPost(int id)
        {
            var command = new RejectPostCommand { PostId = id };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new GetPostDetailsQuery { PostId = id.Value };
            var post = await _mediator.Send(query);
            if (post == null)
            {
                return NotFound();
            }

            if (post.Status == PostStatus.Approved || post.Status == PostStatus.Pending)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["StatusOptions"] = new SelectList(GetPostStatusOptions(), post.Status);
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Status")] PostDto post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (post.Status == PostStatus.Approved)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var command = new UpdatePostCommand { PostId = id, Post = post };
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = new GetPostDetailsQuery { PostId = id.Value };
            var post = await _mediator.Send(query);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeletePostCommand { PostId = id });
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<PostStatus> GetPostStatusOptions()
        {
            return Enum.GetValues(typeof(PostStatus))
                .Cast<PostStatus>()
                .ToList();
        }
    }
}

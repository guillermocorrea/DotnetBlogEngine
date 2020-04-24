using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Infrastructure.Data;
using Application.Repositories;
using static Domain.Post;
using Microsoft.AspNetCore.Authorization;
using WebUI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;

        public PostsController(IPostsRepository postsRepository, ICommentsRepository commentsRepository)
        {
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string status)
        {
            var statusFilter = new List<PostStatus> { PostStatus.Approved };
            if (User.Identity.IsAuthenticated)
            {
                statusFilter.Add(PostStatus.Draft);
                statusFilter.Add(PostStatus.Rejected);
                statusFilter.Add(PostStatus.Pending);
            }
            if (!string.IsNullOrWhiteSpace(status) &&
                User.IsInRole(Roles.Editor) &&
                Enum.TryParse(status, out PostStatus postStatus))
            {
                statusFilter = new List<PostStatus> { postStatus };
            }
            return View(await _postsRepository.GetByStatusAsync(statusFilter.ToArray()));
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.GetAsync(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(new PostDetailsViewModel
            {
                Post = post,
                NewComment = new NewCommentViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(NewCommentViewModel newComment)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = newComment.PostId });
            }
            string username = newComment.Username;
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                username = User.FindFirst(ClaimTypes.Name).Value;
                userId = int.Parse(User.FindFirst("id").Value);
            }
            var comment = new Comment
            {
                PostId = newComment.PostId,
                Content = newComment.Content,
                Username = username,
                UserId = userId
            };
            await _commentsRepository.CreateAsync(comment);
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
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Status,UserId,PublishDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                await _postsRepository.CreateAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.GetAsync(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            if (post.Status == PostStatus.Approved)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["StatusOptions"] = new SelectList(GetPostStatusOptions(), post.Status);
            return View(post);
        }

        [Authorize(Roles = "Editor,Writer")]
        [HttpPost]
        public async Task<IActionResult> SubmitPost(int id)
        {
            var post = await _postsRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Status = PostStatus.Pending;
            await _postsRepository.UpdateAsync(id, post);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> ApprovePost(int id)
        {
            var post = await _postsRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Status = PostStatus.Approved;
            post.PublishDate = DateTime.Now;

            await _postsRepository.UpdateAsync(id, post);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> RejectPost(int id)
        {
            var post = await _postsRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Status = PostStatus.Rejected;

            await _postsRepository.UpdateAsync(id, post);
            return RedirectToAction(nameof(Index));
        }


        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Status")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (post.Status == PostStatus.Approved)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postsRepository.UpdateAsync(id, post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _postsRepository.GetAsync(post.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsRepository.GetAsync(id.Value);
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
            await _postsRepository.RemoveAsync(id);
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

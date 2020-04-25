using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Post;

namespace Application.Posts.Commands.UpdatePost
{
    public class ApprovePostCommand : IRequest
    {
        public int PostId { get; set; }
    }

    public class ApprovePostCommandHandler : IRequestHandler<ApprovePostCommand>
    {
        private readonly IPostsRepository _postsRepository;

        public ApprovePostCommandHandler(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<Unit> Handle(ApprovePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException();
            }

            post.Status = PostStatus.Approved;
            post.PublishDate = DateTime.Now;

            await _postsRepository.UpdateAsync(request.PostId, post);
            return Unit.Value;
        }
    }
}

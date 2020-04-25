using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Post;

namespace Application.Posts.Commands.UpdatePost
{
    public class RejectPostCommand : IRequest
    {
        public int PostId { get; set; }
    }

    public class RejectPostCommandHandler : IRequestHandler<RejectPostCommand>
    {
        private readonly IPostsRepository _postsRepository;

        public RejectPostCommandHandler(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<Unit> Handle(RejectPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postsRepository.GetAsync(request.PostId);
            if (post == null)
            {
                throw new NotFoundException();
            }

            post.Status = PostStatus.Rejected;
            await _postsRepository.UpdateAsync(request.PostId, post);
            return Unit.Value;
        }
    }
}

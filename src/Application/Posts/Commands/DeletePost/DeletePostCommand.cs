using Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public int PostId { get; set; }
    }

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostsRepository _postsRepository;

        public DeletePostCommandHandler(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _postsRepository.RemoveAsync(request.PostId);
            return Unit.Value;
        }
    }
}

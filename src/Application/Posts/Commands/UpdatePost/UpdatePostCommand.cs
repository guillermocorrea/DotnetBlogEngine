using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest
    {
        public int PostId { get; set; }
        public PostDto Post { get; set; }
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IPostsRepository postsRepository, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            if (request.PostId != request.Post.Id)
            {
                throw new NotFoundException();
            }

            var postEntity = _mapper.Map<Post>(request.Post);
            await _postsRepository.UpdateAsync(request.PostId, postEntity);
            return Unit.Value;
        }
    }
}

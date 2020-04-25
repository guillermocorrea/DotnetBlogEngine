using Application.Common.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest
    {
        public PostDto Post { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostsRepository postsRepository,
            IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postEntity = _mapper.Map<Post>(request.Post);
            await _postsRepository.CreateAsync(postEntity);
            return Unit.Value;
        }
    }
}

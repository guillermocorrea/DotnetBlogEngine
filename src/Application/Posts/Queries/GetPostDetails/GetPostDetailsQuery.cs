using Application.Common.Models;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetPostDetails
{
    public class GetPostDetailsQuery : IRequest<PostDto>
    {
        public int PostId { get; set; }
    }

    public class GetPostDetailsHandler : IRequestHandler<GetPostDetailsQuery, PostDto>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IMapper _mapper;

        public GetPostDetailsHandler(IPostsRepository postsRepository, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostDetailsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<PostDto>(await _postsRepository.GetAsync(request.PostId));
        }
    }
}

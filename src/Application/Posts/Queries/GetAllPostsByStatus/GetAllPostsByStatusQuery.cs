using Application.Common.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Domain.Post;

namespace Application.Posts.Queries.GetAllPostsByStatus
{
    public class GetAllPostsByStatusQuery : IRequest<IEnumerable<PostDto>>
    {
        public string Status { get; set; }
    }

    public class GetAllPostsByStatusHandler : IRequestHandler<GetAllPostsByStatusQuery, IEnumerable<PostDto>>
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAllPostsByStatusHandler(IPostsRepository postsRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetAllPostsByStatusQuery request, CancellationToken cancellationToken)
        {
            var statusList = new List<PostStatus> { PostStatus.Approved };
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                statusList.Add(PostStatus.Draft);
                statusList.Add(PostStatus.Rejected);
                statusList.Add(PostStatus.Pending);
            }
            if (!string.IsNullOrWhiteSpace(request.Status) &&
                _httpContextAccessor.HttpContext.User.IsInRole(Roles.Editor) &&
                Enum.TryParse(request.Status, out PostStatus postStatus))
            {
                statusList = new List<PostStatus> { postStatus };
            }
            return _mapper.Map<IEnumerable<PostDto>>(
                await _postsRepository.GetByStatusAsync(statusList.ToArray()));
        }
    }
}

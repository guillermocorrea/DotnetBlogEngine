using Application.Repositories;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public CreateCommentDto NewComment { get; set; }
    }

    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCommentHandler(ICommentsRepository commentsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentsRepository = commentsRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            string username = request.NewComment.Username;
            int? userId = null;
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                username = user.FindFirst(ClaimTypes.Name).Value;
                userId = int.Parse(user.FindFirst("id").Value);
            }
            var comment = new Comment
            {
                PostId = request.NewComment.PostId,
                Content = request.NewComment.Content,
                Username = username,
                UserId = userId
            };
            await _commentsRepository.CreateAsync(comment);
            return Unit.Value;
        }
    }
}

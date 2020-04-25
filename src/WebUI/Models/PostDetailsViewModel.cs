using Application.Comments.Commands.CreateComment;
using Application.Common.Models;

namespace WebUI.Models
{
    public class PostDetailsViewModel
    {
        public PostDto Post { get; set; }
        public CreateCommentDto NewComment { get; set; }
    }
}

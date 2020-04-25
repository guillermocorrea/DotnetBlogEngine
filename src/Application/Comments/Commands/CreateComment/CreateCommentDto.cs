using System.ComponentModel.DataAnnotations;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        [System.ComponentModel.DisplayName("Name")]
        public string Username { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using static Domain.Post;

namespace Application.Common.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PostStatus Status { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public DateTime? PublishDate { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}

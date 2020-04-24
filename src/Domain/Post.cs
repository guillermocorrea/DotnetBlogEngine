using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Post : IEntity<int>
    {
        public enum PostStatus
        {
            Draft,
            Pending,
            Approved,
            Rejected
        }

        public Post()
        {
            Status = PostStatus.Draft;
            Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public PostStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime? PublishDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

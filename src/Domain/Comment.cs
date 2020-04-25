using Domain.Common;
using System;

namespace Domain
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int? UserId { get; set; }
        public int PostId { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

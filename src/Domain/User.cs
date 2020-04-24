using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : IEntity<int>
    {
        public User()
        {
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}

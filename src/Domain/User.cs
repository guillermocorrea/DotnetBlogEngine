using Domain.Common;

namespace Domain
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}

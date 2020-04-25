using Domain.Common;

namespace Domain
{
    public class Role : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class Roles
    {
        public const string Writer = "Writer";
        public const string Editor = "Editor";
    }
}

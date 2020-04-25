namespace Domain.Common
{
    public interface IEntity<Key>
    {
        Key Id { get; set; }
    }
}

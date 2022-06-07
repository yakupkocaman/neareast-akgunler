namespace Akgunler.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}

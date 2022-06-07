namespace Akgunler.Models
{
    public abstract class EntityBaseWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        public abstract TId Id { get; set; }
    }
}

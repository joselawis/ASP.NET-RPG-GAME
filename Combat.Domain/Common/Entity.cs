namespace Combat.Domain.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        private readonly List<DomainEvent> _domainEvents = [];

        protected Entity(TId id)
        {
            Id = id;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Needed for Entity Framework Core
        protected Entity()
        { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
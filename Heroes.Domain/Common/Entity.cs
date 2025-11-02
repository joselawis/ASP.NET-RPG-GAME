namespace Heroes.Domain.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; }
        private readonly List<DomainEvent> _domainEvents = new();

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity()
        { }

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
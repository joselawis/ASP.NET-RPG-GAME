using Heroes.Domain.Common;

namespace Heroes.Domain.Events
{
    public sealed record HeroCreatedEvent(
        Guid HeroId,
        string Name,
        string Class
    ) : DomainEvent;
}
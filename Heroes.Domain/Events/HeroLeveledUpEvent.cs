using Heroes.Domain.Common;

namespace Heroes.Domain.Events
{
    public sealed record HeroLeveledUpEvent(
     Guid HeroId,
     int NewLevel
 ) : DomainEvent;
}
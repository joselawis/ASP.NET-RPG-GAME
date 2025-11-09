using Combat.Domain.Common;

namespace Combat.Domain.Events
{
    public sealed record CombatInitiatedEvent(Guid CombatId, Guid HeroId, Guid EnemyId) : DomainEvent;
}
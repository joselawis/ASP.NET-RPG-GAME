using Combat.Domain.Common;
using Combat.Domain.ValueObjects;

namespace Combat.Domain.Events
{
    public sealed record CombatCompletedEvent(
        Guid CombatId,
        Guid HeroId,
        Guid EnemyId,
        CombatStatus Status,
        Guid WinnerId,
        int ExperienceGained,
        int TurnCount
        ) : DomainEvent;
}
using Combat.Application.DTOs;
using Combat.Domain.Entities;
using Combat.Domain.ValueObjects;

namespace Combat.Application.Mappers
{
    public static class CombatMapper
    {
        public static CombatDto MapToDto(CombatEncounter combat)
        {
            var heroDto = MapToDto(combat.Hero);

            var enemyDto = MapToDto(combat.Enemy);

            var turnsDto = combat.Turns.Select(MapToDto).ToList();

            var experienceGained = combat.Status == CombatStatus.HeroWon
                ? 50 + (combat.Turns.Count * 10)
                : (int?)null;

            return new CombatDto(
                combat.Id.Value,
                heroDto,
                enemyDto,
                combat.Status.ToString(),
                combat.StartedAt,
                combat.EndedAt,
                turnsDto,
                experienceGained);
        }

        public static CombatTurnDto MapToDto(CombatTurn t)
        {
            return new CombatTurnDto(
                            t.TurnNumber,
                            t.AttackerName,
                            t.DefenderName,
                            t.DamageDealt,
                            t.WasCritical);
        }

        public static CombatantDto MapToDto(Combatant combatant)
        {
            return new CombatantDto(
                            combatant.EntityId,
                            combatant.Name,
                            combatant.Type.ToString(),
                            MapToDto(combatant.Stats));
        }

        public static StatsDto MapToDto(CombatantStats stats)
        {
            return new StatsDto(
                                stats.Health,
                                stats.Attack,
                                stats.Defense,
                                stats.Speed);
        }
    }
}
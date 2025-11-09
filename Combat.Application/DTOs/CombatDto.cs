namespace Combat.Application.DTOs
{
    public sealed record CombatDto(
        Guid Id,
        CombatantDto Hero,
        CombatantDto Enemy,
        string Status,
        DateTime StartedAt,
        DateTime? EndedAt,
        List<CombatTurnDto> Turns,
        int? ExperienceGained
    );

    public sealed record CombatantDto(
        Guid EntityId,
        string Name,
        string Type,
        StatsDto Stats
    );

    public sealed record StatsDto(
        int Health,
        int Attack,
        int Defense,
        int Speed
    );

    public sealed record CombatTurnDto(
        int TurnNumber,
        string Attacker,
        string Defender,
        int Damage,
        bool WasCritical
    );
}
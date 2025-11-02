namespace Heroes.Application.DTOs
{
    public sealed record HeroDto(
     Guid Id,
     string Name,
     string Class,
     int Level,
     int Experience,
     StatsDto Stats,
     DateTime CreatedAt,
     DateTime? LastBattleAt
 );

    public sealed record StatsDto(
        int Health,
        int Attack,
        int Defense,
        int Speed
    );
}
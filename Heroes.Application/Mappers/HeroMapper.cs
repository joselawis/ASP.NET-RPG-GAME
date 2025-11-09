using Heroes.Application.DTOs;
using Heroes.Domain.Entities;

namespace Heroes.Application.Mappers
{
    public static class HeroMapper
    {
        public static HeroDto MapToDto(Hero hero)
        {
            return new HeroDto(
                hero.Id.Value,
                hero.Name.Value,
                hero.Class.Name,
                hero.Level.Value,
                hero.Experience.Value,
                new StatsDto(
                    hero.CurrentStats.Health,
                    hero.CurrentStats.Attack,
                    hero.CurrentStats.Defense,
                    hero.CurrentStats.Speed
                ),
                hero.CreatedAt,
                hero.LastBattleAt
            );
        }
    }
}
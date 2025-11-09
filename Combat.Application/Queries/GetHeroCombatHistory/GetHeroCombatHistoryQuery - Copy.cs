using Combat.Application.Common;
using Combat.Application.DTOs;
using Combat.Application.Mappers;
using Combat.Domain.Repositories;
using Combat.Domain.ValueObjects;
using MediatR;

namespace Combat.Application.Queries.GetHeroCombatHistory
{
    public sealed class GetHeroCombatHistoryQueryHandler(ICombatRepository combatRepository) : IRequestHandler<GetHeroCombatHistoryQuery, Result<List<CombatDto>>>
    {
        private readonly ICombatRepository _combatRepository = combatRepository;

        public async Task<Result<List<CombatDto>>> Handle(GetHeroCombatHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var combats = await _combatRepository.GetAllByHeroIdAsync(request.HeroId, cancellationToken);

                var combatDtos = combats.Select(combat =>
                {
                    var heroDto = CombatMapper.MapToDto(combat.Hero);
                    var enemyDto = CombatMapper.MapToDto(combat.Enemy);
                    var turnsDto = combat.Turns.Select(CombatMapper.MapToDto).ToList();

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
                }).ToList();

                return Result<List<CombatDto>>.Success(combatDtos);
            }
            catch (Exception ex)
            {
                return Result<List<CombatDto>>.Failure($"An error occurred while retrieving combat history: {ex.Message}");
            }
        }
    }
}
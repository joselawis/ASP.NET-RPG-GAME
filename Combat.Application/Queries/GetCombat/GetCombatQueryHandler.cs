using Combat.Application.Common;
using Combat.Application.DTOs;
using Combat.Application.Mappers;
using Combat.Domain.Repositories;
using Combat.Domain.ValueObjects;
using MediatR;

namespace Combat.Application.Queries.GetCombat
{
    public sealed class GetCombatQueryHandler(ICombatRepository combatRepository) : IRequestHandler<GetCombatQuery, Result<CombatDto>>
    {
        private readonly ICombatRepository _combatRepository = combatRepository;

        public async Task<Result<CombatDto>> Handle(GetCombatQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var combatId = CombatId.Create(request.CombatId);
                var combat = await _combatRepository.GetByIdAsync(combatId, cancellationToken);

                if (combat is null)
                {
                    return Result<CombatDto>.Failure($"Combat with ID {request.CombatId} not found.");
                }

                var heroDto = CombatMapper.MapToDto(combat.Hero);

                var enemyDto = CombatMapper.MapToDto(combat.Enemy);

                var turnsDto = combat.Turns.Select(CombatMapper.MapToDto).ToList();

                var experienceGained = combat.Status == CombatStatus.HeroWon
                    ? 50 + (combat.Turns.Count * 10)
                    : (int?)null;

                var combatDto = new CombatDto(
                    combat.Id.Value,
                    heroDto,
                    enemyDto,
                    combat.Status.ToString(),
                    combat.StartedAt,
                    combat.EndedAt,
                    turnsDto,
                    experienceGained);

                return Result<CombatDto>.Success(combatDto);
            }
            catch (Exception ex)
            {
                return Result<CombatDto>.Failure($"An error occurred while retrieving combat: {ex.Message}");
            }
        }
    }
}
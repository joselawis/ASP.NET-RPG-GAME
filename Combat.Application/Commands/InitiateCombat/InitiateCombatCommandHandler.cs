using Combat.Application.Common;
using Combat.Application.Common.Interfaces;
using Combat.Application.DTOs;
using Combat.Application.Mappers;
using Combat.Domain.Entities;
using Combat.Domain.Repositories;
using Combat.Domain.ValueObjects;
using MediatR;

namespace Combat.Application.Commands.InitiateCombat
{
    public sealed class InitiateCombatCommandHandler(
        ICombatRepository combatRepository,
        IHeroServiceClient heroServiceClient,
        IDomainEventDispatcher eventDispatcher) : IRequestHandler<InitiateCombatCommand, Result<CombatDto>>
    {
        private readonly ICombatRepository _combatRepository = combatRepository;
        private readonly IHeroServiceClient _heroServiceClient = heroServiceClient;
        private readonly IDomainEventDispatcher _eventDispatcher = eventDispatcher;

        public async Task<Result<CombatDto>> Handle(InitiateCombatCommand request, CancellationToken cancellationToken)
        {
            var heroData = await _heroServiceClient.GetHeroAsync(request.HeroId, cancellationToken);
            if (heroData is null)
            {
                return Result<CombatDto>.Failure($"Hero with ID {request.HeroId} not found.");
            }

            var enemyStats = new CombatantStats(80, 20, 15, 12); // Example enemy stats

            var heroStats = new CombatantStats(
                heroData.Stats.Health,
                heroData.Stats.Attack,
                heroData.Stats.Defense,
                heroData.Stats.Speed);

            var combat = CombatEncounter.Create(
                heroData.Id,
                heroData.Name,
                heroStats,
                request.EnemyId,
                "Goblin", // mocked
                enemyStats);

            combat.ExecuteCombat();

            await _combatRepository.AddAsync(combat, cancellationToken);

            foreach (var domainEvent in combat.DomainEvents)
            {
                await _eventDispatcher.DispatchAsync(domainEvent, cancellationToken);
            }

            combat.ClearDomainEvents();

            var combatDto = CombatMapper.MapToDto(combat);

            return Result<CombatDto>.Success(combatDto);
        }
    }
}
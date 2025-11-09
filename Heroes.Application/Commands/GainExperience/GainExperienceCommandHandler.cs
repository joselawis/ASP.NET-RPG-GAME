using Heroes.Application.Common;
using Heroes.Application.Common.Interfaces;
using Heroes.Domain.Repositories;
using Heroes.Domain.ValueObjects;
using MediatR;

namespace Heroes.Application.Commands.GainExperience
{
    public sealed class GainExperienceCommandHandler : IRequestHandler<GainExperienceCommand, Result>
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public GainExperienceCommandHandler(IHeroRepository heroRepository, IDomainEventDispatcher eventDispatcher)
        {
            _heroRepository = heroRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Result> Handle(GainExperienceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var heroId = HeroId.Create(request.HeroId);
                var hero = await _heroRepository.GetByIdAsync(heroId, cancellationToken);

                if (hero is null)
                {
                    return Result.Failure($"Hero with ID {request.HeroId} not found.");
                }

                // Domain logic
                hero.GainExperience(request.ExperienceAmount);

                // Persist changes
                await _heroRepository.UpdateAsync(hero, cancellationToken);

                // Publish domain events
                foreach (var domainEvent in hero.DomainEvents)
                {
                    await _eventDispatcher.DispatchAsync(domainEvent, cancellationToken);
                }

                hero.ClearDomainEvents();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to gain experience: {ex.Message}");
            }
        }
    }
}
using Heroes.Application.Common;
using Heroes.Application.Common.Interfaces;
using Heroes.Application.DTOs;
using Heroes.Application.Mappers;
using Heroes.Domain.Entities;
using Heroes.Domain.Repositories;
using MediatR;

namespace Heroes.Application.Commands.CreateHero
{
    public sealed class CreateHeroCommandHandler(
        IHeroRepository heroRepository,
        IDomainEventDispatcher eventDispatcher) : IRequestHandler<CreateHeroCommand, Result<HeroDto>>
    {
        private readonly IHeroRepository _heroRepository = heroRepository;
        private readonly IDomainEventDispatcher _eventDispatcher = eventDispatcher;

        public async Task<Result<HeroDto>> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // create the entity
                var hero = Hero.Create(request.Name, request.Class);

                // persist it into the repository
                await _heroRepository.AddAsync(hero, cancellationToken);

                // publish domain events
                foreach (var domainEvent in hero.DomainEvents)
                {
                    await _eventDispatcher.DispatchAsync(domainEvent, cancellationToken);
                }

                hero.ClearDomainEvents();

                // map to DTO
                var heroDo = HeroMapper.MapToDto(hero);

                return Result<HeroDto>.Success(heroDo);
            }
            catch (Exception ex)
            {
                return Result<HeroDto>.Failure($"Failed to create a hero: {ex.Message}");
            }
        }
    }
}
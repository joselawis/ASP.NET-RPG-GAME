using Heroes.Application.Common;
using Heroes.Application.DTOs;
using Heroes.Application.Mappers;
using Heroes.Domain.Repositories;
using Heroes.Domain.ValueObjects;
using MediatR;

namespace Heroes.Application.Queries.GetHero
{
    public sealed class GetHeroQueryHandler(IHeroRepository heroRepository) : IRequestHandler<GetHeroQuery, Result<HeroDto>>
    {
        private readonly IHeroRepository _heroRepository = heroRepository;

        public async Task<Result<HeroDto>> Handle(GetHeroQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var heroId = HeroId.Create(request.HeroId);
                var hero = await _heroRepository.GetByIdAsync(heroId, cancellationToken);

                if (hero is null)
                {
                    return Result<HeroDto>.Failure($"Hero with ID {request.HeroId} not found.");
                }

                var heroDto = HeroMapper.MapToDto(hero);

                return Result<HeroDto>.Success(heroDto);
            }
            catch (Exception ex)
            {
                return Result<HeroDto>.Failure($"Failed to get hero: {ex.Message}");
            }
        }
    }
}
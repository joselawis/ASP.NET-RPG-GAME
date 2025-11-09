using Heroes.Application.Common;
using Heroes.Application.DTOs;
using Heroes.Application.Mappers;
using Heroes.Domain.Repositories;
using MediatR;

namespace Heroes.Application.Queries.GetAllHeroes
{
    public sealed class GetAllHeroesQueryHandler(IHeroRepository heroRepository) : IRequestHandler<GetAllHeroesQuery, Result<List<HeroDto>>>
    {
        private readonly IHeroRepository _heroRepository = heroRepository;

        public async Task<Result<List<HeroDto>>> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var heroes = await _heroRepository.GetAllAsync(cancellationToken);
                var heroDtos = heroes.Select(HeroMapper.MapToDto).ToList();
                return Result<List<HeroDto>>.Success(heroDtos);
            }
            catch (Exception ex)
            {
                return Result<List<HeroDto>>.Failure($"Failed to get all heroes: {ex.Message}");
            }
        }
    }
}
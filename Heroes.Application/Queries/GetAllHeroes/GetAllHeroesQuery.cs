using Heroes.Application.Common;
using Heroes.Application.DTOs;
using MediatR;

namespace Heroes.Application.Queries.GetAllHeroes
{
    public sealed record GetAllHeroesQuery : IRequest<Result<List<HeroDto>>>;
}
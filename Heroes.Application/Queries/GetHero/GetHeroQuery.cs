using Heroes.Application.Common;
using Heroes.Application.DTOs;
using MediatR;

namespace Heroes.Application.Queries.GetHero
{
    public sealed record GetHeroQuery(Guid HeroId) : IRequest<Result<HeroDto>>;
}
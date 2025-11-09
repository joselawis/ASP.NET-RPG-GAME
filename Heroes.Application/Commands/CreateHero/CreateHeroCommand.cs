using Heroes.Application.Common;
using Heroes.Application.DTOs;
using MediatR;

namespace Heroes.Application.Commands.CreateHero
{
    public sealed record CreateHeroCommand(
        string Name,
        string Class
    ) : IRequest<Result<HeroDto>>;
}
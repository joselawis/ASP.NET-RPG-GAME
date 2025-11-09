using Combat.Application.Common;
using Combat.Application.DTOs;
using MediatR;

namespace Combat.Application.Commands.InitiateCombat
{
    public sealed record InitiateCombatCommand(
        Guid HeroId,
        Guid EnemyId
    ) : IRequest<Result<CombatDto>>;
}
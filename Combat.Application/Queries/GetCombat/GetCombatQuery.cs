using Combat.Application.Common;
using Combat.Application.DTOs;
using MediatR;

namespace Combat.Application.Queries.GetCombat
{
    public sealed record GetCombatQuery(Guid CombatId) : IRequest<Result<CombatDto>>;
}
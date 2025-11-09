using Combat.Application.Common;
using Combat.Application.DTOs;
using MediatR;

namespace Combat.Application.Queries.GetHeroCombatHistory
{
    public sealed record GetHeroCombatHistoryQuery(Guid HeroId) : IRequest<Result<List<CombatDto>>>;
}
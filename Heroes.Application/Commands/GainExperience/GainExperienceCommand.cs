using Heroes.Application.Common;
using MediatR;

namespace Heroes.Application.Commands.GainExperience
{
    public sealed record GainExperienceCommand(Guid HeroId, int ExperienceAmount) : IRequest<Result>;
}
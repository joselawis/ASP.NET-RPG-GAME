using FluentValidation;

namespace Heroes.Application.Commands.GainExperience
{
    public sealed class GainExperienceCommandValidator : AbstractValidator<GainExperienceCommand>
    {
        public GainExperienceCommandValidator()
        {
            RuleFor(x => x.HeroId)
                .NotEmpty().WithMessage("Hero ID must not be empty.");
            RuleFor(x => x.ExperienceAmount)
                .GreaterThan(0).WithMessage("Experience amount must be greater than zero.");
        }
    }
}
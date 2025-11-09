using FluentValidation;

namespace Combat.Application.Commands.InitiateCombat
{
    public sealed class InitiateCombatCommandValidator : AbstractValidator<InitiateCombatCommand>
    {
        public InitiateCombatCommandValidator()
        {
            RuleFor(x => x.HeroId)
                .NotEmpty().WithMessage("HeroId must not be empty.");
            RuleFor(x => x.EnemyId)
                .NotEmpty().WithMessage("EnemyId must not be empty.");
            RuleFor(x => x)
                .Must(x => x.HeroId != x.EnemyId)
                .WithMessage("HeroId and EnemyId must be different.");
        }
    }
}
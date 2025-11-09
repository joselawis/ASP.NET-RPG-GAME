using FluentValidation;

namespace Heroes.Application.Commands.CreateHero
{
    public sealed class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
    {
        public CreateHeroCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hero name must not be empty.")
                .MinimumLength(3).WithMessage("Hero name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Hero name must not exceed 50 characters.");

            RuleFor(x => x.Class)
                .NotEmpty().WithMessage("Hero class must not be empty.")
                .Must(BeAValidClass).WithMessage("Hero class must be one of the following: Warrior, Mage, Rogue.");
        }

        private bool BeAValidClass(string className)
        {
            var validClasses = new[] { "warrior", "mage", "rogue" };
            return validClasses.Contains(className.ToLower());
        }
    }
}
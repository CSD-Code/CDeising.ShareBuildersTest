using FluentValidation;

namespace CDeising.ShareBuildersTest.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.StationId)
                .NotEmpty()
                .WithMessage("StationId is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.")
                .MaximumLength(50)
                .WithMessage("First Name must be between 1 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.")
                .MaximumLength(50)
                .WithMessage("Last Name must be between 1 and 50 characters.");
        }
    }
}

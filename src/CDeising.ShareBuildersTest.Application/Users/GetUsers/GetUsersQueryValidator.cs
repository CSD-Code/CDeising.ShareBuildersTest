using FluentValidation;

namespace CDeising.ShareBuildersTest.Application.Users.GetUsers
{
    public class GetUsersValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.StationId)
                .NotEmpty()
                .WithMessage("StationId is required.");
        }
    }
}

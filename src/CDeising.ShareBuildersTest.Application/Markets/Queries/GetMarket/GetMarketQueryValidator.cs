using FluentValidation;

namespace CDeising.ShareBuildersTest.Application.Markets.Queries.GetMarket
{
    public class GetMarketQueryValidator : AbstractValidator<GetMarketQuery>
    {
        public GetMarketQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}

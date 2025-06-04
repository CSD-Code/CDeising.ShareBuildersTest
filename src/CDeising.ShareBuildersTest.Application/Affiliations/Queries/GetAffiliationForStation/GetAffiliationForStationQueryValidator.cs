using FluentValidation;

namespace CDeising.ShareBuildersTest.Application.Affiliations.Queries.GetAffiliationForStation
{
    public class GetAffiliationForStationQueryValidator : AbstractValidator<GetAffiliationForStationQuery>
    {
        public GetAffiliationForStationQueryValidator()
        {
            RuleFor(x => x.StationId)
                .NotEmpty()
                .WithMessage("StationId is required.");
        }
    }
}

using FluentValidation;

namespace CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation
{
    public class GetStationQueryValidator : AbstractValidator<GetStationQuery>
    {
        public GetStationQueryValidator()
        {
            RuleFor(x => x.StationId)
                .NotEmpty()
                .WithMessage("StationId is required.");
        }
    }
}

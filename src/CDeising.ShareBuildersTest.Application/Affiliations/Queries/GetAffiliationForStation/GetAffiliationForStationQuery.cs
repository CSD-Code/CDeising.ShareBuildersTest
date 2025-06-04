using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace CDeising.ShareBuildersTest.Application.Affiliations.Queries.GetAffiliationForStation
{
    public class GetAffiliationForStationQuery : IRequest<IEnumerable<AffiliationDto>>
    {
        public Guid StationId { get; set; }
    }

    public class GetAffiliationQueryHandler : IRequestHandler<GetAffiliationForStationQuery, IEnumerable<AffiliationDto>>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetAffiliationQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<AffiliationDto>> Handle(GetAffiliationForStationQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                return (await dbConnection.QueryAsync<AffiliationDto>(
                    "SELECT [Affiliation].[Id], [Name] " +
                    "FROM [Affiliation] " +
                    "JOIN [StationAffiliation] ON [Affiliation].[Id] = [StationAffiliation].[AffiliationId] " +
                    "WHERE [StationAffiliation].[StationId] = @stationId",
                    new { stationId = request.StationId }))
                    .Select(entity => new AffiliationDto
                    {
                        Id = entity.Id,
                        Name = entity.Name
                    });
            }
        }
    }
}

using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Dapper;
using System.Linq;

namespace CDeising.ShareBuildersTest.Application.Stations.Queries.GetStations
{
    public class GetStationsQuery : IRequest<IEnumerable<StationBriefDto>>
    {

    }

    public class GetStationsQueryHandler : IRequestHandler<GetStationsQuery, IEnumerable<StationBriefDto>>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetStationsQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<StationBriefDto>> Handle(GetStationsQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                return (await dbConnection.QueryAsync<Station>("SELECT Id, CallSign FROM [Station] ORDER BY CallSign"))
                    .Select(entity => new StationBriefDto
                    {
                        Id = entity.Id,
                        CallSign = entity.CallSign
                    });
            }
        }
    }
}

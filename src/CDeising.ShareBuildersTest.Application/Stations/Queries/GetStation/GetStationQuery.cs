using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Dapper;
using System;

namespace CDeising.ShareBuildersTest.Application.Stations.Queries.GetStation
{
    public class GetStationQuery : IRequest<StationDto>
    {
        public Guid StationId { get; set; }
    }

    public class GetStationQueryHandler : IRequestHandler<GetStationQuery, StationDto>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetStationQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<StationDto> Handle(GetStationQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                var entity = await dbConnection.QueryFirstOrDefaultAsync<Station>("SELECT Id, CallSign, Type, MarketId FROM [Station]" +
                    "WHERE Id = @stationId", new { request.StationId });

                return new StationDto
                {
                    Id = entity.Id,
                    CallSign = entity.CallSign,
                    MarketId = entity.MarketId,
                    Type = entity.Type
                };
            }
        }
    }
}

using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Dapper;

namespace CDeising.ShareBuildersTest.Application.Markets.Queries.GetMarket
{
    public class GetMarketQuery : IRequest<MarketDto>
    {
        public Guid Id { get; set; }

    }

    public class GetMarketQueryHandler : IRequestHandler<GetMarketQuery, MarketDto>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetMarketQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<MarketDto> Handle(GetMarketQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                var entity = await dbConnection.QueryFirstOrDefaultAsync<Market>("SELECT [Id], [Name] FROM [Market]" +
                    "WHERE [Id] = @id", new { request.Id });

                return new MarketDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                };

            }
        }
    }
}

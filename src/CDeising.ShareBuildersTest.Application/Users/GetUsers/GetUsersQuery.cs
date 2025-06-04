using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Domain.Entities;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CDeising.ShareBuildersTest.Application.Users.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public Guid StationId { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetUsersQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                return (await dbConnection.QueryAsync<User>("SELECT Id, FirstName, LastName FROM [User] WHERE StationId = @stationId " +
                    "ORDER BY FirstName",
                    new { stationId = request.StationId }))
                    .Select(entity => new UserDto
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    });
            }
        }
    }
}

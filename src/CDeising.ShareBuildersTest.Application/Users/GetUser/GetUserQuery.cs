using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Application.Users.GetUsers;
using CDeising.ShareBuildersTest.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Dapper;

namespace CDeising.ShareBuildersTest.Application.Users.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        readonly IDbConnectionFactory connectionFactory;

        public GetUserQueryHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                var entity = await dbConnection.QueryFirstOrDefaultAsync<User>("SELECT Id, FirstName, LastName FROM [User] WHERE Id = @userId",
                    new { request.UserId });

                return new UserDto
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName
                };
            }
        }
    }
}

using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CDeising.ShareBuildersTest.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public Guid StationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        readonly IDbConnectionFactory connectionFactory;

        public CreateUserCommandHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                Guid id = Guid.NewGuid();

                await dbConnection.ExecuteAsync(
                    "INSERT INTO [User] (Id, FirstName, LastName, StationId) " +
                    "VALUES (@id, @firstName, @lastName, @stationId)",
                    new { id, request.FirstName, request.LastName, request.StationId });

                return id;
            }
        }
    }
}

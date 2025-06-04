using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using Dapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CDeising.ShareBuildersTest.Application.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid StationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        readonly IDbConnectionFactory connectionFactory;

        public UpdateUserCommandHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                await dbConnection.ExecuteAsync(
                    "UPDATE [User] SET FirstName = @firstName, LastName = @lastName, StationId = @stationId WHERE Id = @id", request);
            }
        }
    }
}

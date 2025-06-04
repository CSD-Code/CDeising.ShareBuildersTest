using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Dapper;

namespace CDeising.ShareBuildersTest.Application.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        readonly IDbConnectionFactory connectionFactory;

        public DeleteUserCommandHandler(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbConnection = await connectionFactory.CreateConnectionAsync(cancellationToken))
            {
                await dbConnection.ExecuteAsync("DELETE FROM [User] WHERE Id = @id", new { request.Id });
            }
        }
    }
}

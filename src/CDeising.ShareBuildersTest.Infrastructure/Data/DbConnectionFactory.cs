using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CDeising.ShareBuildersTest.Application.Common.Interfaces;

namespace CDeising.ShareBuildersTest.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        readonly string connectionString;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }
    }
}

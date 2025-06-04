using System.Data;
using System.Threading.Tasks;
using System.Threading;

namespace CDeising.ShareBuildersTest.Application.Common.Interfaces
{
    /// <summary>
    /// Used to retrieve a connection to the DB.
    /// </summary>
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
    }
}

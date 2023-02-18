using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SolutionTemplate.Persistence.Abstractions.Utils;

public interface IConnectionFactory
{
    Task<Connection> Create(CancellationToken ct,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    string GetConnectionString();
}

using System.Data;

namespace SolutionTemplate.Persistence.Abstractions.Utils;

public interface IConnectionFactory
{
    Task<Connection> Create(CancellationToken ct,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    string GetConnectionString();
}

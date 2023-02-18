using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SolutionTemplate.Persistence.Abstractions.Utils;

public sealed class Connection : IAsyncDisposable
{
    public DbConnection DbConnection { get; }
    public DbTransaction? Transaction { get; }
    public CancellationToken CancellationToken { get; }

    private bool _isDisposed;

    public Connection(
        DbConnection dbConnection,
        CancellationToken ct,
        DbTransaction? transaction = null)
    {
        DbConnection = dbConnection;
        CancellationToken = ct;
        Transaction = transaction;

        _isDisposed = false;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
            return;

        try
        {
            if (Transaction is not null)
                await Transaction.CommitAsync(CancellationToken);
        }
        catch (Exception)
        {
            if (Transaction is not null)
                await Transaction.RollbackAsync(CancellationToken);

            throw;
        }
        finally
        {
            await DbConnection.CloseAsync();

            _isDisposed = true;
        }
    }
}

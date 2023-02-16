using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Persistence;

public sealed class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Connection> Create(
        CancellationToken ct,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        var connection = GetConnection();
        await connection.OpenAsync(ct);

        var transaction = await connection.BeginTransactionAsync(isolationLevel, ct);

        return new Connection(connection, ct, transaction);
    }

    private DbConnection GetConnection()
    {
        var connectionString = GetConnectionString();
        var connection = new NpgsqlConnection(connectionString);

        return connection;
    }

    public string GetConnectionString()
    {
        var defaultDatabase = _configuration.GetRequiredSection("DefaultDatabase");

        return new NpgsqlConnectionStringBuilder
        {
            Host = defaultDatabase["Host"],
            Port = int.TryParse(defaultDatabase["Port"], out var port)
                ? port
                : 5432,
            Username = defaultDatabase["Username"],
            Password = defaultDatabase["Password"],
            Database = defaultDatabase["DatabaseName"]
        }.ToString();
    }
}

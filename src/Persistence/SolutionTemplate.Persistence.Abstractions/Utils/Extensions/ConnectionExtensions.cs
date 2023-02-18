using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace SolutionTemplate.Persistence.Abstractions.Utils.Extensions;

public static class ConnectionExtensions
{
    public static async Task<IReadOnlyList<TResult>> QueryAsync<TResult>(
        this Connection connection,
        string sql,
        object? parameters = null)
    {
        var query = BuildCommand(sql, parameters, connection.CancellationToken, connection.Transaction);
        var result = await connection.DbConnection.QueryAsync<TResult>(query);

        return result.AsList();
    }
    public static async Task<TResult> QuerySingleAsync<TResult>(
        this Connection connection,
        string sql,
        object? parameters = null)
    {
        var query = BuildCommand(sql, parameters, connection.CancellationToken, connection.Transaction);
        var result = await connection.DbConnection.QuerySingleAsync<TResult>(query);

        return result;
    }

    public static async Task<int> ExecuteAsync(
        this Connection connection,
        string sql,
        object? parameters = null)
    {
        var command = BuildCommand(sql, parameters, connection.CancellationToken, connection.Transaction);
        return await connection.DbConnection.ExecuteAsync(command);
    }

    public static async Task<TResult> ExecuteScalarAsync<TResult>(
        this Connection connection,
        string sql,
        object? parameters = null)
    {
        var command = BuildCommand(sql, parameters, connection.CancellationToken, connection.Transaction);
        return await connection.DbConnection.ExecuteScalarAsync<TResult>(command);
    }

    private static CommandDefinition BuildCommand(
        string commandText,
        object? parameters,
        CancellationToken ct,
        DbTransaction? transaction = null,
        CommandType commandType = CommandType.Text,
        int timeout = 60) =>
        new(
            commandText: commandText,
            parameters: parameters,
            commandType: commandType,
            commandTimeout: timeout,
            transaction: transaction,
            cancellationToken: ct);
}

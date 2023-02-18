using System;
using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions;
using SolutionTemplate.Persistence.Abstractions.Utils;
using SolutionTemplate.Persistence.Abstractions.Utils.Extensions;

namespace SolutionTemplate.Persistence;

public sealed class PointRepository : IPointRepository
{
    public async Task<long> Add(long orderId, Point point, Connection connection)
    {
        const string command = @"
INSERT INTO point(order_id, note)
VALUES (@orderId, @note)
RETURNING id
";
        var parameters = new { orderId, note = point.Note };

        return await connection.ExecuteScalarAsync<long>(command, parameters);
    }
}

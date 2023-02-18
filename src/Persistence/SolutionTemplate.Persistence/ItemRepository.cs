using System;
using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions;
using SolutionTemplate.Persistence.Abstractions.Utils;
using SolutionTemplate.Persistence.Abstractions.Utils.Extensions;

namespace SolutionTemplate.Persistence;

public sealed class ItemRepository : IItemRepository
{
    public async Task<long> Add(long orderId, Item item, Connection connection)
    {
        const string command = @"
INSERT INTO item(order_id, note)
VALUES (@orderId, @note)
RETURNING id
";
        var parameters = new { orderId, note = item.Note };

        return await connection.ExecuteScalarAsync<long>(command, parameters);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions;
using SolutionTemplate.Persistence.Abstractions.Utils;
using SolutionTemplate.Persistence.Abstractions.Utils.Extensions;

namespace SolutionTemplate.Persistence;

public sealed class OrderRepository : IOrderRepository
{
    public async Task<long> Add(Order order, Connection connection)
    {
        const string command = @"
INSERT INTO orders(note) 
VALUES(@note)
RETURNING id
";
        var parameters = new { note = order.Note };

        return await connection.ExecuteScalarAsync<long>(command, parameters);
    }

    public async Task<Order> GetById(long id, Connection connection)
    {
        const string queryOrder = @"
SELECT id as id,
       note as note
FROM orders WHERE id = @orderId;
";
        const string queryPoints = @"
SELECT id as id, 
       note as note 
FROM point WHERE order_id = @orderId;
";
        const string queryItems = @"
SELECT id as id, 
       note as note 
FROM item WHERE order_id = @orderId;
";
        var parameters = new { orderId = id };
        var points = await connection.QueryAsync<Point>(queryPoints, parameters);
        var items = await connection.QueryAsync<Item>(queryItems, parameters);
        var order = await connection.QuerySingleAsync<Order>(queryOrder, parameters);

        order = MapToOrder(order, points, items);

        return order;
    }

    private Order MapToOrder(Order order, IReadOnlyList<Point> points, IReadOnlyList<Item> items) =>
        new(order.Id, order.Note, items, points);
}

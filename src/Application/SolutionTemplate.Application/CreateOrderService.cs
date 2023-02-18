using SolutionTemplate.Application.Abstractions;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Application;

public sealed class CreateOrderService : ICreateOrderService
{
    private readonly IItemRepository _itemRepository;
    private readonly IPointRepository _pointRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IConnectionFactory _connectionFactory;

    public CreateOrderService(
        IItemRepository itemRepository,
        IPointRepository pointRepository,
        IOrderRepository orderRepository,
        IConnectionFactory connectionFactory)
    {
        _itemRepository = itemRepository;
        _pointRepository = pointRepository;
        _orderRepository = orderRepository;
        _connectionFactory = connectionFactory;
    }

    public async Task<Order> Create(string? note, List<Item> items, List<Point> points, CancellationToken ct)
    {
        await using var connection = await _connectionFactory.Create(ct);
        var tempOrder = Order.Create(note, items, points);
        var orderId = await _orderRepository.Add(tempOrder, connection);

        foreach (var point in points)
            await _pointRepository.Add(orderId, point, connection);

        foreach (var item in items)
            await _itemRepository.Add(orderId, item, connection);

        return await _orderRepository.GetById(orderId, connection);
    }
}

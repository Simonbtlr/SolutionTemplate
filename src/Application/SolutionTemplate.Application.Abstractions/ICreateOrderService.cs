using SolutionTemplate.Domain;

namespace SolutionTemplate.Application.Abstractions;

public interface ICreateOrderService
{
    Task<Order> Create(
        string? note,
        List<Item> items,
        List<Point> points, 
        CancellationToken ct);
}

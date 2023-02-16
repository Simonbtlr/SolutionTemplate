using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Persistence.Abstractions;

public interface IOrderRepository
{
    Task<long> Add(Order order, Connection connection);
    Task<Order> Get(long id, Connection connection);
    Task<IReadOnlyList<Order>> GetByIds(IEnumerable<long> ids, Connection connection);
    Task Update(Order order, Connection connection);
    Task Delete(long id, Connection connection);
}

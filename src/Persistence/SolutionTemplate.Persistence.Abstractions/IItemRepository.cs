using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Persistence.Abstractions;

public interface IItemRepository
{
    Task<long> Add(long orderId, Item item, Connection connection);
}

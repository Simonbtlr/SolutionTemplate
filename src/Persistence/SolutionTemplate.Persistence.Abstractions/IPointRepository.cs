using System.Threading.Tasks;
using SolutionTemplate.Domain;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Persistence.Abstractions;

public interface IPointRepository
{
    Task<long> Add(long orderId, Point item, Connection connection);
}

using Microsoft.Extensions.DependencyInjection;
using SolutionTemplate.Persistence;
using SolutionTemplate.Persistence.Abstractions;
using SolutionTemplate.Persistence.Abstractions.Utils;
using SolutionTemplate.Persistence.Utils;

namespace SolutionTemplate.Modules;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services
            .AddScoped<IConnectionFactory, ConnectionFactory>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IPointRepository, PointRepository>()
            .AddScoped<IItemRepository, ItemRepository>()
        ;
}

using SolutionTemplate.Persistence;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Modules;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services
            .AddScoped<IConnectionFactory, ConnectionFactory>();
}

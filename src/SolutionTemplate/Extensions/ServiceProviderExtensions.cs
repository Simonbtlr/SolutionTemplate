using System;
using Microsoft.Extensions.DependencyInjection;
using SolutionTemplate.Persistence.Abstractions.Utils;

namespace SolutionTemplate.Extensions;

public static class ServiceProviderExtensions
{
    public static string GetMasterConnectionString(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var connectionFactory = scope.ServiceProvider.GetRequiredService<IConnectionFactory>();

        return connectionFactory
            .GetConnectionString();
    }
}

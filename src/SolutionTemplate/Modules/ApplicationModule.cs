using Microsoft.Extensions.DependencyInjection;
using SolutionTemplate.Application;
using SolutionTemplate.Application.Abstractions;

namespace SolutionTemplate.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddScoped<ICreateOrderService, CreateOrderService>()
        ;
}

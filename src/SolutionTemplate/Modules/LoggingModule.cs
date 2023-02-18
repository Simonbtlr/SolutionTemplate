using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static Serilog.Log;

namespace SolutionTemplate.Modules;

public static class LoggingModule
{
    public static void AddCustomLogging(this IServiceCollection services, IConfiguration configuration) =>
        Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
}

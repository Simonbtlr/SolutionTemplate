using Serilog;
using SolutionTemplate.Modules;

namespace SolutionTemplate;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomLogging(_configuration);

        services
            .AddPersistence()
            ;
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
    }
}

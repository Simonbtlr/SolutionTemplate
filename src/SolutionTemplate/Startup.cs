using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SolutionTemplate.Grpc;
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
        services.AddControllers();
        services.AddGrpc();
        services.AddGrpcReflection();
        
        services.AddCustomLogging(_configuration);

        services
            .AddPersistence()
            .AddApplication()
            ;
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<SolutionTemplateService>();
            endpoints.MapGrpcReflectionService();
        });
    }
}

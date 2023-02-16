using Serilog;
using SolutionTemplate;
using SolutionTemplate.Extensions;
using SolutionTemplate.Persistence.Migrations;

var hostBuilder = Host
    .CreateDefaultBuilder(args)
    .UseDefaultServiceProvider(opts =>
    {
        opts.ValidateScopes = true;
        opts.ValidateOnBuild = true;
    })
    .ConfigureWebHostDefaults(cfg =>
    {
        cfg.UseStartup<Startup>();
    })
    .UseSerilog();
var host = hostBuilder.Build();

await host.RunWithMigrationAsync(
    args,
    typeof(Init).Assembly,
    GetMasterConnectionString);

string GetMasterConnectionString(IServiceProvider serviceProvider) =>
    serviceProvider.GetMasterConnectionString();

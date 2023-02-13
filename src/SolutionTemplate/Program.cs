using SolutionTemplate;

var hostBuilder = Host
    .CreateDefaultBuilder(args)
    .UseDefaultServiceProvider(opts =>
    {
        opts.ValidateScopes = true;
        opts.ValidateOnBuild = true;
    })
    .ConfigureWebHostDefaults(cfg => cfg.UseStartup<Startup>());
var host = hostBuilder.Build();

await host.RunAsync();

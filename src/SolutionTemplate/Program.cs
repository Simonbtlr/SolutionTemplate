using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
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
        cfg.ConfigureKestrel(opts =>
        {
            var port = Environment.GetEnvironmentVariable("ASPNETCORE_GRPC_PORT")
                       ?? throw new ArgumentNullException("ASPNETCORE_GRPC_PORT");

            opts.Listen(IPAddress.Any, int.Parse(port), lOpts =>
            {
                lOpts.Protocols = HttpProtocols.Http2;
            });
        });
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

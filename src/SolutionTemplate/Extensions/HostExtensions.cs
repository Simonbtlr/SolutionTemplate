using System.Reflection;
using FluentMigrator.Runner;
using ILogger = Serilog.ILogger;

namespace SolutionTemplate.Extensions;

public static class HostExtensions
{
    public static async Task RunWithMigrationAsync(
        this IHost host,
        string[] args,
        Assembly migrationAssembly,
        Func<IServiceProvider, string> connectionStringFactory
    )
    {
        var action = args.IsMigrate()
            ? host.MigrateAsync(args, migrationAssembly, connectionStringFactory)
            : host.RunAsync();

        await action;
    }
    
    private static Task MigrateAsync(
        this IHost host,
        string[] args,
        Assembly migrationsAssembly,
        Func<IServiceProvider, string> connectionStringFactory)
    {
        var conStr = connectionStringFactory(host.Services);
        var serviceProvider = GetCorrectServiceProvider(conStr, migrationsAssembly);

        Migrate(serviceProvider, args);
        
        return Task.CompletedTask;
    }
    
    private static ServiceProvider GetCorrectServiceProvider(
        string conStr,
        Assembly migrationsAssembly) =>
        new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(runnerBuilder =>
                runnerBuilder
                    .AddPostgres()
                    .WithGlobalConnectionString(conStr)
                    .ScanIn(migrationsAssembly).For.Migrations())
            .AddLogging(loggingBuilder => loggingBuilder.AddFluentMigratorConsole())
            .BuildServiceProvider();


    
    private static void Migrate(ServiceProvider serviceProvider, string[] args)
    {
        var logger = SerilogExtensions.CreateLogger();
        
        if (!IsMigrate(args, out var dryRun))
            return;

        logger.Information("Migration starting...");

        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        if (dryRun)
            DryRunMigrate(runner, logger);
        else
            Migrate(runner, logger);
    }
    
    private static void DryRunMigrate(IMigrationRunner runner, Serilog.ILogger logger)
    {
        logger.Information("Getting information about migrations...");

        if (!runner.HasMigrationsToApplyUp())
        {
            logger.Information("Already up to date!");

            return;
        }

        logger.Information("Pending migrations...");
        runner.ListMigrations();

        logger.Information("Success!");
    }
    
    private static void Migrate(IMigrationRunner runner, ILogger logger)
    {
        if (!runner.HasMigrationsToApplyUp())
        {
            logger.Information("Already up to date!");
            return;
        }

        logger.Information("Database updating...");

        runner.MigrateUp();

        logger.Information("Database updated!");
    }
    
    private static bool IsMigrate(string[] args, out bool dryRun)
    {
        dryRun = false;

        if (args is { Length: <= 0 } || args[0] is not "migrate")
            return false;

        if (args is { Length: > 1 } && args[1] is "--dryrun")
            dryRun = true;

        return true;
    }
    
    private static bool IsMigrate(this string[] args) => 
        args is { Length: > 0 } 
        && args[0] is "migrate"
        ;
}

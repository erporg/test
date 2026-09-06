using System.Diagnostics;
using ERP.ControlPlane.Tenants.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ERP.ControlPlane.Tenants.MigrationWorker;

public sealed class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using Activity? activity = s_activitySource.StartActivity(nameof(ExecuteAsync), ActivityKind.Client);

        try
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            (dbContext, cancellationToken),
            static async (state, _) => { await state.dbContext.Database.MigrateAsync(state.cancellationToken); },
            cancellationToken);
    }

    private static async Task SeedDataAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        // TODO: create models

        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            (dbContext, cancellationToken),
            static async (state, token) =>
            {
                await using IDbContextTransaction transaction =
                    await state.dbContext.Database.BeginTransactionAsync(state.cancellationToken);

                // TODO: add models
                await state.dbContext.SaveChangesAsync(state.cancellationToken);
                await transaction.CommitAsync(state.cancellationToken);
            },
            cancellationToken);
    }
}

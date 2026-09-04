using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Shared.Migrators
{
    public class DatabaseMigrator<TContext>(
        IServiceProvider serviceProvider,
        ILogger<DatabaseMigrator<TContext>> logger) : IHostedService
        where TContext : DbContext
    {
        private const int MaxAttempts = 10;
        private static readonly TimeSpan Delay = TimeSpan.FromSeconds(5);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            for (int attempt = 1; attempt <= MaxAttempts; attempt++)
            {
                try
                {
                    logger.LogInformation(
                        "Applying migrations for {DbContext} (Attempt {Attempt}/{MaxAttempts})...",
                        typeof(TContext).Name, attempt, MaxAttempts);

                    await context.Database.MigrateAsync(cancellationToken);

                    logger.LogInformation("Database migration succeeded on attempt {Attempt}", attempt);

                    return;
                }
                catch (Exception ex) when (attempt < MaxAttempts)
                {
                    logger.LogWarning(
                        ex,
                        "Failed to apply migrations for {DbContext} on attempt {Attempt}/{MaxAttempts}. Retrying in {Delay} seconds...",
                        typeof(TContext).Name, attempt, MaxAttempts, Delay.TotalSeconds);

                    await Task.Delay(Delay, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Database migration failed after {Max} attempts, giving up", MaxAttempts);
                    throw;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

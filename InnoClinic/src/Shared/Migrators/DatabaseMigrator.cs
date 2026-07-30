using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InnoClinic.Shared.Migrators
{
    public class DatabaseMigrator<TContext>(IServiceProvider serviceProvider) : IHostedService where TContext : DbContext
    {
        private const int MaxAttempts = 10;
        private static readonly TimeSpan Delay = TimeSpan.FromSeconds(5);
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            for (int attempt = 1; attempt <= MaxAttempts; attempt ++)
            {
                try
                {
                    await context.Database.MigrateAsync(cancellationToken);
                    return;
                }
                catch (Exception ex) when (attempt < MaxAttempts)
                {
                    await Task.Delay(Delay, cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

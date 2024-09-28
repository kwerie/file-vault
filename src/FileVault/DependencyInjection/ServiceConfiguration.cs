using Microsoft.EntityFrameworkCore;

namespace FileVault.DependencyInjection;

public static class ServiceConfiguration
{
    public static void SetupDatabase(this IServiceCollection serviceCollection, IConfiguration configuration, bool isDevelopment)
    {
        serviceCollection.AddDbContext<FileVaultDatabaseContext>(
            dbContextOptions =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var mariaDbServerVersion = Version.TryParse(configuration["MariaDbVersion"], out var mariaDbVersion)
                    ? new MariaDbServerVersion(mariaDbVersion)
                    : ServerVersion.AutoDetect(connectionString);
                var opt = dbContextOptions
                    .UseMySql(connectionString, mariaDbServerVersion);

                if (isDevelopment)
                {
                    opt
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            }
        );
    }
}

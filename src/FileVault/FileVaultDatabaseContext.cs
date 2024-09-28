using FileVault.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = FileVault.Entities.File;

namespace FileVault;

public class FileVaultDatabaseContext(DbContextOptions<FileVaultDatabaseContext> options) : IdentityDbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSnakeCaseNamingConvention();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x is { Entity: HasTimestamps, State: EntityState.Added or EntityState.Modified });

        foreach (var entity in entities)
        {
            var now = DateTimeOffset.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((HasTimestamps)entity.Entity).CreatedAt = now;
            }

            ((HasTimestamps)entity.Entity).UpdatedAt = now;
        }
    }
}
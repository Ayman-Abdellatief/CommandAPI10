using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }
   public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var entries = ChangeTracker.Entries()
        .Where(e => e.Entity is ICreatedAtTrackable && e.State == EntityState.Added);

    foreach (var entry in entries)
    {
        ((ICreatedAtTrackable)entry.Entity).CreatedAt = DateTime.UtcNow;
    }

    return base.SaveChangesAsync(cancellationToken);
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configuring one-to-many relationship
    modelBuilder.Entity<Platform>()
        .HasMany(p => p.Commands)
        .WithOne(c => c.Platform)
        .HasForeignKey(c => c.PlatformId)
        .OnDelete(DeleteBehavior.Cascade);
            
    modelBuilder.Entity<Command>()
        .HasIndex(c => c.PlatformId)
        .HasDatabaseName("Index_Command_PlatformId");
}
}
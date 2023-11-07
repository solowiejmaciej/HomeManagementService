#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace HomeManagementService.Models;

public class AutomationsDbContext : DbContext
{
    public AutomationsDbContext(DbContextOptions<AutomationsDbContext> options) : base(options)
    {
    }
    public DbSet<Automation?> Automations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Automation>()
            .ToContainer("Automations")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.Id);
    }
}
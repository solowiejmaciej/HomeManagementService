#region

using Microsoft.EntityFrameworkCore;

#endregion

namespace HomeManagementService.Models;

public class AutomationsDbContext : DbContext
{
    public DbSet<Automation?> Automations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(
            "https://hms-db.documents.azure.com:443/",
            "ARRZYtbCZYRCS81AawChqVx91njbCqb7yfdPzPa5q94twZJDd50jzOIDHQCqHTGApaTEbMKLeGgVACDbWl6LXQ==",
            "HMSDB01"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Automation>()
            .ToContainer("Automations")
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.Id);
    }
}
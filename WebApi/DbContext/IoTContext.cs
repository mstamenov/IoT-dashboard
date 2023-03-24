using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

public class IoTContext : DbContext
{
    public IoTContext(DbContextOptions<IoTContext> options) : base(options)
    {
    }

    public DbSet<Telemetry> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Telemetry>().ToTable("Enina");
    }
}
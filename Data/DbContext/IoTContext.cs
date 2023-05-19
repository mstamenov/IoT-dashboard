using IoT.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IoT.Data;

internal class IoTContext : DbContext, IIoTContext
{
    public IoTContext(DbContextOptions<IoTContext> options) : base(options)
    {
    }

    public DbSet<Telemetry> Telemetries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Telemetry>().ToTable("Telemetry");
    }
}

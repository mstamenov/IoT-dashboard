using IoT.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IoT.Data
{
    public interface IIoTContext
    {
        DbSet<Telemetry> Telemetries { get; set; }
    }
}

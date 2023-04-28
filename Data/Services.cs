using IoT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IIoTContext, IoTContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("IoTContext")));

        }
    }
}

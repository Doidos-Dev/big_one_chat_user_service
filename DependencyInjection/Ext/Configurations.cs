using Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjection.Ext
{
    public static class Configurations
    {
        public static void AddConfig(this IServiceCollection services)
        {
            services
                .DatabaseConfiguration();
        }
        static IServiceCollection DatabaseConfiguration(this IServiceCollection service)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER");

            service.AddDbContext<ContextCommand>(opt => opt.UseNpgsql(connectionString!));
            service.AddSingleton(p => new ContextRead(connectionString!));
            
            return service;
        }
    }
}

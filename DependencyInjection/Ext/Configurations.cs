using Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Generic;
using Data.Repositories.Generic;
using Domain.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using Data;

namespace DependencyInjection.Ext
{
    public static class Configurations
    {
        public static void AddConfig(this IServiceCollection services,IConfiguration configuration)
        {
            services
                .DatabaseConfiguration(configuration)
                .AddRepositories();
        }
        static IServiceCollection DatabaseConfiguration(this IServiceCollection service , IConfiguration connection)
        {
            string connectionString =
                Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER") ?? connection.GetConnectionString("Local")!;

            service.AddDbContext<ContextCommand>(opt => opt.UseNpgsql(connectionString));

            service.AddSingleton(p => new ContextRead(connectionString));
            
            return service;
        }

        static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();

            return services;
        }
    }
}

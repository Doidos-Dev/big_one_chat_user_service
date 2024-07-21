using Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Generic;
using Data.Repositories.Generic;
using Domain.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using Application.Services;
using Application.Services.Implementations;

namespace DependencyInjection.Ext
{
    public static class Configurations
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .DatabaseConfiguration(configuration)
                .AddRepositories()
                .AddServices();
        }
        static IServiceCollection DatabaseConfiguration(this IServiceCollection service, IConfiguration connection)
        {
            string connectionString =
                Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER") ?? connection.GetConnectionString("Local")!;

            service.AddDbContextPool<DatabaseContext>(opt => opt.UseNpgsql(connectionString));

            return service;
        }

        static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();

            return services;
        }

        static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISettingsService, SettingsService>();

            return services;
        }
    }
}

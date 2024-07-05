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
        public static void AddConfig(this IServiceCollection services, string applicationStage)
        {
            services
                .DatabaseConfiguration(applicationStage)
                .AddRepositories();
        }
        static IServiceCollection DatabaseConfiguration(this IServiceCollection service, string applicationStage)
        {

            service.AddDbContext<ContextCommand>(opt => opt.UseNpgsql(applicationStage == DataConfigurations.LocalEnvironment
                ? Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER")!
                : Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER_LOCAL")!));

            //Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER_LOCAL"));

            service.AddSingleton(p => new ContextRead(applicationStage == DataConfigurations.LocalEnvironment
                ? Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER")!
                : Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER_LOCAL")!));
            
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

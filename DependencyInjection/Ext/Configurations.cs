using Data.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Generic;
using Data.Repositories.Generic;
using Domain.Interfaces;
using Data.Repositories;

namespace DependencyInjection.Ext
{
    public static class Configurations
    {
        public static void AddConfig(this IServiceCollection services, string connectionString)
        {
            services
                .DatabaseConfiguration(connectionString)
                .AddRepositories();
        }
        static IServiceCollection DatabaseConfiguration(this IServiceCollection service, string connectionString)
        {

            Console.WriteLine(connectionString);

            service.AddDbContext<ContextCommand>(opt => opt.UseNpgsql(connectionString!));
            service.AddSingleton(p => new ContextRead(connectionString!));
            
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

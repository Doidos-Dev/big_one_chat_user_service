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
using Application.Validations;
using FluentValidation;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Security;
using Application.Security.Implementations;

namespace DependencyInjection.Ext
{
    public static class Configurations
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .DatabaseConfiguration(configuration)
                .AddRepositories()
                .AddServices()
                .AddValidation()
                .AddAuth(configuration);
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
            services.AddScoped<IUserService, UserService>()
            .AddScoped<ISettingsService, SettingsService>()
            .AddScoped<ICriptographyAss, CriptographyAss>();

            return services;
        }

        static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserCreateValidaton>(ServiceLifetime.Singleton)
                .AddValidatorsFromAssemblyContaining<UserUpdateValidaton>(ServiceLifetime.Singleton)
                .AddValidatorsFromAssemblyContaining<SettingsUpdateValidation>(ServiceLifetime.Singleton);

            return services;
        }

        static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            string keyEnv = Environment.GetEnvironmentVariable("AUTH_KEY") ?? configuration["AUTH:SecretKey"]!;

            var key = Encoding.ASCII.GetBytes(keyEnv);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}

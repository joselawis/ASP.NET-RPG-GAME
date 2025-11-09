using Heroes.Application.Common.Interfaces;
using Heroes.Domain.Repositories;
using Heroes.Infrastructure.Messaging;
using Heroes.Infrastructure.Persistence;
using Heroes.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heroes.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // database
            services.AddDbContext<HeroesDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("HeroesDb");
                options.UseNpgsql(connectionString);
            });

            // repositories
            services.AddScoped<IHeroRepository, HeroRepository>();

            // MassTransit and RabbitMQ
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    var rabbitMqHost = configuration["RabbitMQ:Host"] ?? "localhost";
                    var rabbitMqUsername = configuration["RabbitMQ:Username"] ?? "guest";
                    var rabbitMqPassword = configuration["RabbitMQ:Password"] ?? "guest";

                    cfg.Host(rabbitMqHost, "/", h =>
                    {
                        h.Username(rabbitMqUsername);
                        h.Password(rabbitMqPassword);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            // Domain event dispatcher
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            return services;
        }
    }
}
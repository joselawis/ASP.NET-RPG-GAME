using Combat.Application.Common.Interfaces;
using Combat.Domain.Repositories;
using Combat.Infrastructure.Messaging;
using Combat.Infrastructure.Persistence;
using Combat.Infrastructure.Persistence.Repositories;
using Combat.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Combat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // database
            services.AddDbContext<CombatDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CombatDb");
                options.UseNpgsql(connectionString);
            });

            // repositories
            services.AddScoped<ICombatRepository, CombatRepository>();

            // HTTP client for Heroes service
            services.AddHttpClient<IHeroServiceClient, HeroServiceClient>(client =>
            {
                var heroesServiceUrl = configuration["Services:HeroesApi"] ?? "https://localhost:5001";
                client.BaseAddress = new Uri(heroesServiceUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

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
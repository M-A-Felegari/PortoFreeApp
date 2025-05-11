using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Interfaces.Messaging;
using PortoFree.Infrastructure.Messaging.Consumers;

namespace PortoFree.Infrastructure.Messaging;

public static class MessagingServiceCollectionExtensions
{
    public static void AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<DeleteImageConsumer>();

            cfg.UsingInMemory((context, config) =>
            {
                config.ConfigureEndpoints(context);
            });
        });
        
        services.AddScoped<IEventPublisher, EventPublisher>();
    }
}
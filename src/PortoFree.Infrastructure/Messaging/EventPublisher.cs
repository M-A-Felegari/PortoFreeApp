using MassTransit;
using PortoFree.Application.Interfaces.Messaging;

namespace PortoFree.Infrastructure.Messaging;

public class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    public EventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    public async Task PublishAsync<TEvent>(TEvent eventMessage, CancellationToken cancellationToken = default)
    {
        if (eventMessage == null)
            throw new ArgumentNullException(nameof(eventMessage));
        
        await _publishEndpoint.Publish(eventMessage, cancellationToken);
    }
}
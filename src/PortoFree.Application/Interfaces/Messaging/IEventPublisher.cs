namespace PortoFree.Application.Interfaces.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent eventMessage, CancellationToken cancellationToken = default);
}
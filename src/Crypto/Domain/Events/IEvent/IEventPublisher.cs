using Hangfire.Storage.Monitoring;

namespace CryptoExchange.Events;
public interface IEventPublisher
{
    Task PublishAsync<T>(T domainEvent, CancellationToken ct = default)  where T : class;
}
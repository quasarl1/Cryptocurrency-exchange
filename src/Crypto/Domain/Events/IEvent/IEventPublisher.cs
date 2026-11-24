using System.Threading;
using Hangfire.Storage.Monitoring;

namespace CryptoExchange.Events;
public interface IEventPublisher
{
    Task PublishAsync(UserRegisteredEvent domainEvent, CancellationToken ct = default );
}
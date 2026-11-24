namespace CryptoExchange.Events;
public interface IAuditableInterface
{
    string UserId { get; }
    string Action { get; }
    DateTime? Registered {get;}
    string IpAddress { get; }
}
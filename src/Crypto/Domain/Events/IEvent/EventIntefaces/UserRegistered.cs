using CryptoExchange.Models;
using CryptoExchange.EventService;
namespace CryptoExchange.Events;
public record UserRegisteredEvent : IAuditableInterface
{
    public string UserId { get; init; }
    public string? IpAddress { get; init; }
    public string? Email { get; init; }
    public string? PublicName { get; init; }
    public string? UserTag { get; init; }
    public DateTime RegistrationTime { get; init; }
    public string Action => "UserRegistered";

    public DateTime? Registered => throw new NotImplementedException();

    public UserRegisteredEvent(string userId, DateTime registrationTime) 
    {
        UserId = userId;
        RegistrationTime = registrationTime;
    }
}

public interface IAuditService
    {
         Task LogAsync<T>(T audit, CancellationToken token = default) where T : class;
    }
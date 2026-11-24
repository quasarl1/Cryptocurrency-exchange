using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace CryptoExchange.Models;
public enum Type
{
    UserRegistered
}
public class OutboxMessage
{
    // HAVE TO ADD 
    // string? UserTag and more from a User model
    public string Id { get; private set; }
    public string UserId {get;private set;}
    public string Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    /* public string IpAdress {get; private set;} */
    // have to add VO for checking ip
    public int RetryCount { get; private set; }
    public string? ErrorMessage { get; private set; }
    private OutboxMessage() { }
    public OutboxMessage(string? userId, string type)
    {
        UserId = userId!;
        Id = "outb_" + Guid.CreateVersion7();
        Type = type;
        CreatedAt = DateTime.UtcNow;
        ProcessedAt = null;
        RetryCount = 0;
        ErrorMessage = null;
    }


    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
    }
        public void MarkAsFailed(string error)
    {
        RetryCount++;
        ErrorMessage = "Error";
    }
}
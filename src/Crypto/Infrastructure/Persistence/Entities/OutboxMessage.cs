using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace CryptoExchange.Models;
public class OutboxMessage
{
    public string Id { get; private set; }
    public string Type { get; private set; }
    public string Load {get; private set;}
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    /* public string IpAdress {get; private set;} */
    // have to add VO for checking ip
    public int RetryCount { get; private set; }
    public string? ErrorMessage { get; private set; }
    private OutboxMessage() { }
    public OutboxMessage(string type, /* string ipAddress */ string load)
    {
        Id = "outb_" + Guid.CreateVersion7();
        Type = type;
        Load = load;
        // IpAdress = ipAddress;
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
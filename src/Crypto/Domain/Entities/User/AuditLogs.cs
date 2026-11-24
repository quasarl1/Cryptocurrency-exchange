namespace CryptoExchange.Models;
public class UserAuditLogs
{
    public string Id { get; private set; }
    public string UserId { get; private set; }
    public string Action { get; private set; }
    public string IpAddress { get; private set; }
    public DateTime RegistrationTime { get; private set; }


    public UserAuditLogs(string userId,string action,string ipAddress){
        Id = "audit_" + Guid.NewGuid().ToString();
        UserId = userId;
        Action = action;
        IpAddress = ipAddress;
        RegistrationTime = DateTime.UtcNow; 
    }

    private UserAuditLogs() { }
}
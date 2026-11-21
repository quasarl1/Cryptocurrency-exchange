namespace CryptoExchange.Models;
public class UserAuditLogs
{
    public string Userid {get; private set;}
    public string Action { get; private set; }
    public DateTime?  RegistrationTime {get; private set;} = DateTime.UtcNow;
    public string Id {get; private set;}
    public string IpAdress {get; private set;}

}
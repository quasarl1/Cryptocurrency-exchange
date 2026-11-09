using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models;
public class Account
{
    public string AccountId {get;private set;}
    public string UserID {get;private set;}
    public User User {get;private set;} = null!;
    public bool IsFrozen {get;private set;} = false;
    public DateTime FreezingTime {get;private set;}
    private Account() {}

    public Account(string userId, bool isFrozen, DateTime freezingTime)
    {
        AccountId = Guid.CreateVersion7().ToString();
        UserID = userId;
        IsFrozen = isFrozen;
        FreezingTime = freezingTime;
    }
}
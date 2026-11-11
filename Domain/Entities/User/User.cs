using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models;
public class User
{
    public PublicName PublicName {get; private set;}
    public string Tag {get; private set;}
    public Email Email {get; private set;}
    public string Id {get; private set;}
    public bool IsBlocked{get;private set;} = false;

    private User() { }

public User(Email email, PublicName publicName,string tag, bool isBlocked)
    {
        Id = Guid.CreateVersion7().ToString();
        Email = email;
        PublicName = publicName;
        Tag = tag;
        IsBlocked = isBlocked;
    } 
}
//add second User for containing hashes BCRYPT  =====> Authorization

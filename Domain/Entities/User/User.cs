using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models;
public class User
{
    public PublicName PublicName {get; private set;}
    public string Tag {get; private set;}
    public Email Email {get; private set;}
    public string Id {get; private set;}

           private User() { }

public User(Email email, PublicName publicName,string tag)
    {
        Id = Guid.NewGuid().ToString();
        Email = email;
        PublicName = publicName;
        Tag = tag;

    } 
}

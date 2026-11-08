using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models;
public class User
{
    public string PublicName {get; private set;}
    public string Tag {get; private set;}
    public Email Email {get; set;}
    public string Id {get; private set;}

           private User() { }

public User(Email email)
    {
        Id = Guid.NewGuid().ToString();
        Email = email;
        
                if (string.IsNullOrEmpty(Id))
          {
              throw new ValidationException("id can't be empty");
          }

    } 
}

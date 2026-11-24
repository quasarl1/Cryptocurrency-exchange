using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Models;
public class User
{
    public PublicName PublicName {get; private set;}
    public UserTag Tag {get; private set;}
    public Email Email {get; private set;}
    public string Id {get; private set;}
    public DateTime RegistrationTime {get;private set;}
    public bool IsBlocked{get;private set;} = false;

    private User() { }

public User(Email email, PublicName publicName,UserTag tag, bool isBlocked)
    {
        Email = email;
        PublicName = publicName;
        Tag = tag;
        IsBlocked = isBlocked;
        Id = "usr" + Guid.CreateVersion7();
    }

}
//add second User for containing hashes BCRYPT  =====> Authorization

public class AuthUser
{
    public string Id {get;private set;}
    public string UserId {get; private set;}
    public Login Login {get;private set;}
    public string Password {get;private set;}

    public AuthUser(string _Userid, Login _Login, string _Password)
    {
        Id = "auth_" + Guid.CreateVersion7();
        UserId = _Userid;
        Login = _Login;
        Password = _Password;
    }

}
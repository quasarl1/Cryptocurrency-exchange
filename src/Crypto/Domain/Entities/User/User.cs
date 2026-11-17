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
        Id = "usr_" + Guid.CreateVersion7();
        Email = email;
        PublicName = publicName;
        Tag = tag;
        IsBlocked = isBlocked;
    } 
}
//add second User for containing hashes BCRYPT  =====> Authorization

public class AuthUser
{
    public string UserId {get; private set;}
    public string Login {get;private set;}
    public string Password {get;private set;}

    public AuthUser(string _Userid, string _Login, string _Password)
    {
        UserId = _Userid;
        Login = _Login;
        Password = _Password;
    }

}
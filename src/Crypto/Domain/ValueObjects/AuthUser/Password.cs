using System.ComponentModel.DataAnnotations;
using System.Configuration;
namespace CryptoExchange;
public sealed record Password
{
    public string Value {get;}

public Password(string value)
    {
        if(string.IsNullOrWhiteSpace(value)) throw new ValidationException("Password can't be empty");
        var normalized = value.Trim();

        if(normalized.Length > 64 || normalized.Length < 8) throw new ValidationException("Password have to be between 8 and 64 characters");
        
        //add regex for checking on special characteres

        Value = normalized;
    }
}
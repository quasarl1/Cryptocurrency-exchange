using System;
using System.ComponentModel.DataAnnotations;
namespace CryptoExchange;
public sealed record Email
{
    public string Value {get;}
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value))
           {
                         throw new ValidationException("Email is required"); 
           }
        // removing spaces + To lower
        // убираем пробелы "  email@gmail.com"  "email@gmail.com " + делаем нижний регистр
        var normalized = value?.Trim().ToLowerInvariant();
        if (normalized.Length > 254 || !normalized.Contains("@"))  
           { 
                         throw new ValidationException("Email isn't right");
           }
        Value = normalized;
    }
}
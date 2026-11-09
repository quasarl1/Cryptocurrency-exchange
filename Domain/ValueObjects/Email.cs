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
        // убираем пробелы  + делаем нижний регистр
        // Example --- Пример

        // "  email@gmail.com"  "email@gmail.com "
        var normalized = value.Trim().ToLowerInvariant();
        if (normalized.Length > 254 || !normalized.Contains("@"))  
           { 
                         throw new ValidationException("the Length of a Email can't be more than 32 symbols or has to contain '@' ");
           }
        Value = normalized;
    }
}
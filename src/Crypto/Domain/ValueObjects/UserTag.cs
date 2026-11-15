using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;

namespace CryptoExchange;
public sealed record UserTag
{
    // forbid symbols

    //запрет символов

    private static readonly Regex Valid = new(@"^[a-zA-Z0-9_-]{3,30}$", RegexOptions.Compiled);
    public string Value {get;}
    public UserTag(string value)
    {
                    if (string.IsNullOrEmpty(value))
               {
                    throw new ValidationException("Tag is required");
               }
        var normalized = value.Trim();

        // IsMatch for checking content

        // IsMatch для проверки содержимого
        
                    if(normalized.Length > 32 || Valid.IsMatch(normalized))
              {
                    throw new ValidationException("the Length of a Tag can't be more than 32 symbols or can't contain ' '^[a-zA-Z0-9](),-,$");
              }
        Value = normalized;
    }
}
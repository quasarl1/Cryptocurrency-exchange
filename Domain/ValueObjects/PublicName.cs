using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
namespace CryptoExchange;
public sealed record PublicName
{
    public string Value {get;}
    //   Hashset + 
//        StringComparer(Oridinal) = by bytes +
//           IgnoreCase:
// "Admin" = true
// "admin" = true
// "aDmin" = true
private static HashSet<string> _reserved = new(StringComparer.OrdinalIgnoreCase);
    public PublicName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new ValidationException("PublicName can't be empty");
        }
        var normalized = value.Trim();

                if (_reserved.Contains(normalized))

            throw new ValidationException("This name is reserved");

        Value = normalized;
    }
    public static void LoadReserved(IEnumerable<string> names)

        => _reserved = new HashSet<string>(names, StringComparer.OrdinalIgnoreCase);
}
// have to add 'REGEX' for checking 'PublicName' on containing forbidden symbols
using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;
namespace CryptoExchange;
public sealed record PublicName
{
    private static readonly Regex Valid = new(@"^[a-zA-Z0-9_-]{3,30}$", RegexOptions.Compiled);
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
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException("PublicName can't be empty");
        }
        var normalized = value.Trim();

                if (_reserved.Contains(normalized)) throw new ValidationException("This name is reserved");
                if(!Valid.IsMatch(normalized)) throw new ValidationException("the Name can't contain =!@#$%^&*()_-+=&,.';][{}]");
        Value = normalized;
    }
    public static void LoadReserved(IEnumerable<string> names)

        => _reserved = new HashSet<string>(names, StringComparer.OrdinalIgnoreCase);
}
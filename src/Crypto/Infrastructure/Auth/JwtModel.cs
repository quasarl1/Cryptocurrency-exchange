using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Data;
    public class JwtOptions
    {
        public const string SelectionName = "Jwt";
        [Required]
        [MinLength(32,ErrorMessage = "password too short")]
        [MaxLength(500, ErrorMessage = "password is too very big!")]
        public string Key {get;init;} = string.Empty;
        [Range(1,770)]
        public int ExpiresHours { get; init; } = 24;
    }
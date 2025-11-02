using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Sentinel.Api.Services
{
    public class PasswordGeneratorService(IOptions<PasswordOptions> passwordOptions)
    {
        private const string Uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowers = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string Symbols = "!@#$%^&*()-_=+[]{};:,.<>?";

        public string Generate(int? minLength = null, string? allowedSymbols = null)
        {
            var options = passwordOptions.Value;

            var length = Math.Max(options.RequiredLength, minLength ?? 16);
            var symbols = allowedSymbols ?? Symbols;

            var required = new List<char>();
            void AddFrom(string s) => required.Add(s[RandomNumberGenerator.GetInt32(s.Length)]);

            if (options.RequireUppercase) AddFrom(Uppers);
            if (options.RequireLowercase) AddFrom(Lowers);
            if (options.RequireDigit) AddFrom(Digits);
            if (options.RequireNonAlphanumeric) AddFrom(symbols);

            var pool = string.Concat(
                options.RequireUppercase ? Uppers : "",
                options.RequireLowercase ? Lowers : "",
                options.RequireDigit ? Digits : "",
                options.RequireNonAlphanumeric ? symbols : ""
            );
            if (pool.Length == 0) pool = Uppers + Lowers + Digits + symbols;

            var chars = new List<char>(required);
            while (chars.Count < length)
                chars.Add(pool[RandomNumberGenerator.GetInt32(pool.Length)]);

            if (options.RequiredUniqueChars > 1)
            {
                while (chars.Distinct().Count() < options.RequiredUniqueChars)
                    chars.Add(pool[RandomNumberGenerator.GetInt32(pool.Length)]);
            }

            // Fisher–Yates
            for (int i = chars.Count - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars.ToArray());
        }
    }
}

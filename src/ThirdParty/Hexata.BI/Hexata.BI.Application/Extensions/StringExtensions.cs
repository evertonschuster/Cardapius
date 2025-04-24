using System.Globalization;
using System.Text;

namespace Hexata.BI.Application.Extensions
{
    public static class StringExtensions
    {
        public static string? Sanitizing(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            var s = value.Trim();
            s = s.TrimEnd('.');

            if (s.Length == 0)
                return default;

            return char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant();
        }

        public static string RemoveCorrupted(this string input)
        {
            var cleanedInput = new StringBuilder();
            foreach (char c in input)
            {
                if (Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || Char.IsPunctuation(c))
                {
                    cleanedInput.Append(c);
                }
            }
            return cleanedInput.ToString();
        }

        public static string RemoveAccents(this string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var result = stringBuilder.ToString();

            return result.Replace("ã", "a")
                        .Replace("õ", "o")
                        .Replace("é", "e")
                        .Replace("í", "i")
                        .Replace("ó", "o")
                        .Replace("ú", "u")
                        .Replace("á", "a")
                        .Replace("â", "a")
                        .Replace("à", "a")
                        .Replace("ê", "e")
                        .Replace("é", "e")
                        .Replace("ô", "o")
                        .Replace("î", "i")
                        .Replace("ú", "u")
                        .Replace("ç", "c")
                        .Replace("í", "i");
        }
    }
}

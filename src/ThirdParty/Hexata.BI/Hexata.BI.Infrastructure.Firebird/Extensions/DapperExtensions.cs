using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Hexata.Infrastructure.Firebird.Extensions
{
    public static class DapperExtensions
    {
        public static T? GetValueOrDefault<T>(this IDataReader reader, string columnName, T? defaultValue = default)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                    return defaultValue;

                var value = reader.GetValue(ordinal);
                var targetType = typeof(T);

                if (targetType.IsEnum)
                    return ParseEnum<T>(value) ?? defaultValue;

                var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
                return (T)Convert.ChangeType(value, underlyingType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter valor da coluna '{columnName}'", ex);
            }
        }

        private static T? ParseEnum<T>(object value)
        {
            var enumType = typeof(T);

            if (value is string str)
            {
                foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttr?.Name?.ToUpper() == str.ToUpper() || field.Name.ToUpper() == str.ToUpper())
                        return (T)field.GetValue(null)!;
                }
            }

            if (value is int intValue && Enum.IsDefined(enumType, intValue))
            {
                return (T)Enum.ToObject(enumType, intValue);
            }

            return default;
        }

    }
}

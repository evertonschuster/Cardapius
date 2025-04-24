using Dapper;
using Hexata.BI.Application.Extensions;
using System.Data;

namespace Hexata.Infrastructure.Firebird.SqlMappers
{
    internal class SanitizingStringHandler : SqlMapper.TypeHandler<string?>
    {
        public override string? Parse(object value)
        {
            if (value == null || value is DBNull)
                return default;

            if (value is string text)
            {
                return text.Sanitizing();
            }

            return default;
        }

        public override void SetValue(IDbDataParameter parameter, string? value)
        {
            parameter.Value = value;
            parameter.DbType = DbType.String;
        }
    }

}

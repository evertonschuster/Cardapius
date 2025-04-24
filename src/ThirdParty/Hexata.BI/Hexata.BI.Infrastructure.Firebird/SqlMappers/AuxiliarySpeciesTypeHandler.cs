using Dapper;
using Hexata.BI.Application.Extensions;
using System.Data;

namespace Hexata.Infrastructure.Firebird.SqlMappers
{
    internal class AuxiliarySpeciesTypeHandler : SqlMapper.TypeHandler<List<string>>
    {
        private const string _delimiter = "->";

        public override List<string> Parse(object value)
        {
            if (value == null || value is DBNull)
                return new List<string>();

            var raw = value.ToString()!;

            return raw
                .Split(new[] { _delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(token => token
                    .Trim()
                    .TrimEnd('.')
                    .ToUpperInvariant()
                    .Sanitizing()!
                )
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }

        public override void SetValue(IDbDataParameter parameter, List<string> list)
        {
            var joined = list
                .Select(s => s + "..")
                .Select(s => _delimiter + " " + s)
                .Aggregate((a, b) => a + "  " + b);

            parameter.Value = joined;
            parameter.DbType = DbType.String;
        }
    }
}
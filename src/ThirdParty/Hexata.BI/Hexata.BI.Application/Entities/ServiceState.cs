using Hexata.BI.Application.DataBaseSyncs;

namespace Hexata.BI.Application.Entities
{
    public class ServiceState
    {
        public required string Id { get; set; }

        public required SyncDto? State { get; set; }

        public string? Error { get; set; }

        public required DateTimeOffset LastSync { get; set; }

        public DateTimeOffset? LastError { get; set; }
    }
}

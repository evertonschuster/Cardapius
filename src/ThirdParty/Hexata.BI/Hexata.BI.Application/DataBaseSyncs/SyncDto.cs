namespace Hexata.BI.Application.DataBaseSyncs
{
    public class SyncDto
    {
        public DateTime Reference { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        internal static SyncDto Create()
        {
            return new SyncDto()
            {
                Reference = DateTime.UtcNow,
                Page = 1,
                PageSize = 100
            };
        }
    }
}

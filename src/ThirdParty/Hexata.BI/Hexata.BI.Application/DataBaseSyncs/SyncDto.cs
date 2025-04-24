namespace Hexata.BI.Application.DataBaseSyncs
{
    public class SyncDto
    {
        public DateTime Reference { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 100;
    }
}

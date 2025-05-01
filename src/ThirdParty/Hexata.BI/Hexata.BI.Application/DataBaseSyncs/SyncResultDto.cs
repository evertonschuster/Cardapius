using Hexata.BI.Application.Dtos;

namespace Hexata.BI.Application.DataBaseSyncs
{
    public class SyncResultDto
    {
        public DateTime Reference { get; private set; }

        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public bool IsLastPage { get; private set; }

        internal static Result<SyncResultDto, SyncStatus> DonetPage(SyncDto syncDto)
        {
            return new SyncResultDto
            {
                Reference = syncDto.Reference,
                Page = syncDto.Page,
                PageSize = syncDto.PageSize,
                IsLastPage = false
            };
        }

        internal static Result<SyncResultDto, SyncStatus> DoneLastPage(SyncDto syncDto)
        {
            return new SyncResultDto
            {
                Reference = syncDto.Reference,
                Page = syncDto.Page,
                PageSize = syncDto.PageSize,
                IsLastPage = true
            };
        }

        internal SyncDto ToSyncDto()
        {
            return new SyncDto()
            {
                Reference = Reference,
                Page = Page,
                PageSize = PageSize
            };
        }
    }
}

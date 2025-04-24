using Hexata.BI.Application.Dtos;

namespace Hexata.BI.Application.DataBaseSyncs
{
    public interface ISyncService
    {
        Task<Result<SyncResultDto, SyncStatus>> SyncAsync(SyncDto syncDto, CancellationToken cancellationToken);
    }
}

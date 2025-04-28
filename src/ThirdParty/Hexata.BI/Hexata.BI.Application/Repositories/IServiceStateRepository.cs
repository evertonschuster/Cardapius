
using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.Entities;

namespace Hexata.BI.Application.Repositories
{
    public interface IServiceStateRepository
    {
        Task<ServiceState?> GetStateAsync(string serviceName, CancellationToken cancellationToken);
        Task SaveErrorAsync(string serviceName, object error, CancellationToken cancellationToken);
        Task SaveStateAsync(string serviceName, SyncDto syncDto, CancellationToken cancellationToken);
    }
}

using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hexata.Infrastructure.SqlLite.Repositories
{
    internal class ServiceStateRepository(AppDbContext appContext) : IServiceStateRepository
    {
        public Task<ServiceState?> GetStateAsync(string serviceName, CancellationToken cancellationToken)
        {
            serviceName = serviceName.ToLowerInvariant();

            return appContext.ServiceStates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == serviceName, cancellationToken);
        }

        public async Task SaveErrorAsync(string serviceName, object error, CancellationToken cancellationToken)
        {
            serviceName = serviceName.ToLowerInvariant();
            var entity = await appContext.ServiceStates
                .FirstOrDefaultAsync(x => x.Id == serviceName, cancellationToken);


            if (entity != null)
            {
                entity.Error = JsonConvert.SerializeObject(error);
                entity.LastError = DateTime.UtcNow;
                appContext.ServiceStates.Update(entity);
            }
            else
            {
                entity = new ServiceState
                {
                    Id = serviceName,
                    State = entity?.State,
                    LastSync = DateTime.UtcNow,
                    LastError = DateTime.UtcNow,
                    Error = JsonConvert.SerializeObject(error),
                };
                appContext.ServiceStates.Add(entity);
            }

            await appContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveStateAsync(string serviceName, SyncDto syncDto, CancellationToken cancellationToken)
        {
            serviceName = serviceName.ToLowerInvariant();
            var entity = await appContext.ServiceStates
                .FirstOrDefaultAsync(x => x.Id == serviceName, cancellationToken);


            if (entity is not null)
            {
                entity.State = syncDto;
                entity.LastSync = DateTime.UtcNow;

                appContext.ServiceStates.Update(entity);
            }
            else
            {
                entity = new ServiceState
                {
                    Id = serviceName,
                    State = syncDto,
                    LastSync = DateTime.UtcNow
                };

                appContext.ServiceStates.Add(entity);
            }

            await appContext.SaveChangesAsync();
        }
    }
}

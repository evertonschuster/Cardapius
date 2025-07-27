using BuildingBlock.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Interceptors
{
    public class SoftDeleteInterceptor(IUserContext userContext) : SaveChangesInterceptor
    {
        private const string DeletedAtPropertyName = nameof(ISoftDelete.DeletedAt);
        private const string DeletedByPropertyName = nameof(ISoftDelete.DeletedBy);

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted(eventData);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void MarkSoftDeleted(DbContextEventData eventData)
        {
            if (eventData.Context is not null)
            {
                var now = DateTimeOffset.UtcNow;

                var entries = eventData.Context.ChangeTracker
                        .Entries()
                        .Where(e => e.State == EntityState.Deleted && e.Metadata.FindProperty(DeletedAtPropertyName) is not null);

                foreach (var softDeletable in entries)
                {
                    softDeletable.State = EntityState.Modified;
                    softDeletable.CurrentValues[DeletedAtPropertyName] = now;
                    softDeletable.CurrentValues[DeletedByPropertyName] = userContext.UserId;
                }
            }
        }
    }
}

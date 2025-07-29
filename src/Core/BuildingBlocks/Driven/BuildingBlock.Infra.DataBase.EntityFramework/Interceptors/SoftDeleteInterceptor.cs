using BuildingBlock.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Interceptors
{
    public class SoftDeleteInterceptor(IUserContext userContext) : SaveChangesInterceptor
    {
        private const string DeletedAtPropertyName = nameof(ISoftDelete.DeletedAt);
        private const string DeletedByPropertyName = nameof(ISoftDelete.DeletedBy);

        /// <summary>
        /// Invoked after changes have been saved to the database. Calls the base implementation without additional logic.
        /// </summary>
        /// <param name="eventData">Contextual information about the completed save operation.</param>
        /// <param name="result">The result returned by the save operation.</param>
        /// <returns>The result of the save operation.</returns>
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return base.SavedChanges(eventData, result);
        }

        /// <summary>
        /// Handles post-save operations asynchronously, ensuring entities marked for deletion are processed as soft deletes before completing the save.
        /// </summary>
        /// <param name="eventData">The event data associated with the completed save operation.</param>
        /// <param name="result">The result of the save operation.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task representing the asynchronous operation, containing the number of state entries written to the database.</returns>
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted(eventData);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Intercepts the asynchronous save operation to apply soft delete logic before changes are persisted.
        /// </summary>
        /// <param name="eventData">The event data associated with the save operation.</param>
        /// <param name="result">The interception result for the save operation.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="ValueTask{InterceptionResult}"/> representing the interception result.</returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Converts entities marked for deletion into soft deletes by updating their deletion metadata instead of removing them from the database.
        /// </summary>
        /// <param name="eventData">The event data containing the current DbContext and change tracker.</param>
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

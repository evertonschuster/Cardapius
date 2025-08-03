namespace BuildingBlock.Domain
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync(CancellationToken? cancellationToken = null);

        void Rollback();

        Task RollbackAsync(CancellationToken? cancellationToken = null);
    }
}

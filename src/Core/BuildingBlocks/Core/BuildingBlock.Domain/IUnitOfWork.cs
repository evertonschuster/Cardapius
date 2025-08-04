namespace BuildingBlock.Domain
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();

        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}

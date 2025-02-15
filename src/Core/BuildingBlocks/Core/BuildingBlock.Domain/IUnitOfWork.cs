namespace BuildingBlock.Domain
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();

        void Rollback();

        Task RollbackAsync();
    }
}

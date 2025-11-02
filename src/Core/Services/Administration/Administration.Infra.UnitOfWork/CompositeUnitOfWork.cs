using BuildingBlock.Application;

namespace Administration.Infra.UnitOfWork
{

    public sealed class CompositeUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork _sqlUow;
        private readonly IUnitOfWork _mongoUow;

        public CompositeUnitOfWork(IUnitOfWork sqlUow, IUnitOfWork mongoUow)
        {
            _sqlUow = sqlUow;
            _mongoUow = mongoUow;
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await _sqlUow.CommitAsync(cancellationToken);
                total += await _mongoUow.CommitAsync(cancellationToken);

                return total;
            }
            catch
            {
                try
                {
                    await _sqlUow.RollbackAsync(cancellationToken);
                }
                catch
                {
                    /* log */
                }
                try
                {
                    await _mongoUow.RollbackAsync(cancellationToken);
                }
                catch
                {
                    /* log */
                }

                throw;
            }
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _sqlUow.RollbackAsync(cancellationToken);
            }
            catch
            {
                /* log */
            }
            try
            {
                await _mongoUow.RollbackAsync(cancellationToken);
            }
            catch
            {
                /* log */
            }
        }
    }
}

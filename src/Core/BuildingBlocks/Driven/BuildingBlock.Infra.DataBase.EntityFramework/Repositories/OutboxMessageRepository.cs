using BuildingBlock.Application.Entities;
using BuildingBlock.Application.Repositories;

namespace BuildingBlock.Infra.DataBase.EntityFramework.Repositories
{
    internal class OutboxMessageRepository(IDbContext dbContext) : IOutboxMessageRepository
    {
        public void Insert(List<OutboxMessageEntity> outboxMessages)
        {
            var appDbContext = (AppDbContext)dbContext;
            appDbContext.OutboxMessageEntities.AddRange(outboxMessages);
        }

        public void Update(List<OutboxMessageEntity> outboxMessages)
        {
            var appDbContext = (AppDbContext)dbContext;
            appDbContext.OutboxMessageEntities.UpdateRange(outboxMessages);
        }
    }
}
using Hexata.BI.Application.Entities;
using Hexata.BI.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hexata.Infrastructure.SqlLite.Repositories
{
    internal class MonthlyConsumptionRepository(AppDbContext appContext) : IMonthlyConsumptionRepository
    {
        public async Task AddByMonthAsync(string key, DateTime reference)
        {
            var updated = await appContext.MonthlyConsumptions
                .Where(x => x.Id == key && x.Month.Year == reference.Year && x.Month.Month == reference.Month)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.Total, p => p.Total + 1));

            if (updated == 0)
            {
                var newConsumption = new MonthlyConsumption(key, reference, 1);
                await appContext.MonthlyConsumptions.AddAsync(newConsumption);
            }

            await appContext.SaveChangesAsync();
        }

        public Task<MonthlyConsumption?> GetByMonthAsync(string key, DateTime reference)
        {
            return appContext.MonthlyConsumptions
                .Where(x => x.Id == key && x.Month.Year == reference.Year && x.Month.Month == reference.Month)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}

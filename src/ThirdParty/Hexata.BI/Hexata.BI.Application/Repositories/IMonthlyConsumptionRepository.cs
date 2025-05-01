using Hexata.BI.Application.Entities;

namespace Hexata.BI.Application.Repositories
{
    public interface IMonthlyConsumptionRepository
    {
        Task AddByMonthAsync(string key, DateTime reference);

        Task<MonthlyConsumption?> GetByMonthAsync(string key, DateTime reference);
    }
}

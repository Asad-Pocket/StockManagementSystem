using StockManagementSystem.Models;
using StockManagementSystem.Repositories;

namespace StockManagementSystem.Repositories
{
    public interface ISoldItemRepository : IRepository<StockOutRecord>
    {
        List<StockOutRecord> SearchByDate(DateTime fromdate, DateTime Todate);
    }
}
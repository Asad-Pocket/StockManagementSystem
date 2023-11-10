using StockManagementSystem.Data;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;

namespace StockManagementSystem.Repositories
{
    public class SoldItemRepository : Repository<StockOutRecord>, ISoldItemRepository
    {
        public SoldItemRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public List<StockOutRecord> SearchByDate(DateTime fromdate, DateTime Todate)
        {
            List<StockOutRecord> Items = _ctx.Records.Where(item => item.Date >= fromdate
            && item.Date <= Todate && item.StockOutType == StockOutType.Sell).ToList();

            return Items; 
        }
    }
}
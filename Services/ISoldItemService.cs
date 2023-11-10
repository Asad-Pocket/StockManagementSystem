using StockManagementSystem.Models;

namespace StockManagementSystem.Services
{
    public interface ISoldItemService
    {
        void Create(List<StockOutRecord> item);
        public List<StockOutRecord> GetAll();
        public List<StockOutRecord> GetItemQuantity(DateTime formattedDate, DateTime formattedDate2);

    }
}

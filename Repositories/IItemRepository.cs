using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        public IEnumerable<Item> GetAllInItem();
        public IEnumerable<Item> GetItemByCompany(int id);
        public IEnumerable<Item> GetItemByCompanyAndCategory(int ComId, int catId);
        public IEnumerable<Item> GetItemByCategory(int id);
    }

}

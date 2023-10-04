using StockManagementSystem.Repositories;

namespace StockManagementSystem.UnitOfWorks
{
    public interface IApplicationUnitOfWorks:IUnitOfWorks
    {
        public IItemRepository _items { get; }
        public ICategoryrepository _categories { get; }
        public ICompanyRepository _companies { get; }
    }
}

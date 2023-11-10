using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;

namespace StockManagementSystem.UnitOfWorks
{
    public class ApplicationUnitOfWorks : UnitOfWorks, IApplicationUnitOfWorks
    {
        public IItemRepository _items { get; private set; }
        public ICategoryrepository _categories { get; private set; }
        public ICompanyRepository _companies { get; private set; }
        public ISoldItemRepository _soldItems { get; private set; } 

        public ApplicationUnitOfWorks(ApplicationDbContext dbContext, ISoldItemRepository soldItems ,ICategoryrepository categories, ICompanyRepository companies, IItemRepository items) : base((DbContext)dbContext)
        {
            _items = items;
            _categories = categories;
            _companies = companies;
            _soldItems = soldItems;
        }
    }
}
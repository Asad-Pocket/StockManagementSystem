using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockManagementSystem.Data;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public class CategoryRepository : Repository<Category> , ICategoryrepository
    {
        public CategoryRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
using StockManagementSystem.Data;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public class CompanyRepository : Repository<Company> , ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace StockManagementSystem.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWorks
    {
        protected readonly DbContext _dbContext;

        public UnitOfWorks(DbContext dbContext) => _dbContext = dbContext;

        public virtual void Dispose() => _dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();
    }
}

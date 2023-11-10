using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.UnitOfWorks;
using CategoryBo = StockManagementSystem.BusinessObject.Category;
using CategoryEo = StockManagementSystem.Models.Category;
namespace StockManagementSystem.Service
{
    public interface ICategoryService 
    {
        IEnumerable<CategoryBo> GetAllItem();
        CategoryEo GetById(int id);
        void Create(CategoryBo item);
        void Update(CategoryBo _item);
        void Delete(int id);
    }
}

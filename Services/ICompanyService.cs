using StockManagementSystem.Models;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
namespace StockManagementSystem.Service
{
    public interface ICompanyService
    {
        IEnumerable<CompanyBo> GetAllItem();
        CompanyEo GetById(int id);
        void Create(CompanyBo item);
        void Update(CompanyBo _item);
        void Delete(int id);
    }
}

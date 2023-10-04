using AutoMapper;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.UnitOfWorks;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
namespace StockManagementSystem.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IApplicationUnitOfWorks _UnitOfWork;
        private readonly IMapper _mapper;
        public CompanyService(IApplicationUnitOfWorks works,IMapper mapper) 
        {
            _UnitOfWork = works;
            _mapper = mapper;
        }

        public void Create(CompanyBo item)
        {
            CompanyEo companyEo = _mapper.Map<CompanyEo>(item);
            _UnitOfWork._companies.Insert(companyEo);
            _UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            _UnitOfWork._companies.Delete(id);
            _UnitOfWork.Save();
        }

        public IEnumerable<CompanyBo> GetAllItem()
        {
            var _clist = _UnitOfWork._companies.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CompanyEo>, List<CompanyBo>>(_clist);
            return mappedList;
        }
        public CompanyEo GetById(int id)
        {
            var item = _UnitOfWork._companies.GetById(id);
            return item;
        }

        public void Update(CompanyBo _item)
        {
            var _company = _UnitOfWork._companies.GetById(_item.Id);
            _company = _mapper.Map(_item, _company);
            _UnitOfWork._companies.Update(_company);
            _UnitOfWork.Save();
        }
    }
}

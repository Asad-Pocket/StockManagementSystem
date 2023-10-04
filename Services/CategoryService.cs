using AutoMapper;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.UnitOfWorks;
using System;
using CategoryBo = StockManagementSystem.BusinessObject.Category;
using CategoryEo = StockManagementSystem.Models.Category;
namespace StockManagementSystem.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationUnitOfWorks _UnitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IApplicationUnitOfWorks _works, IMapper mapper)
        { 
            _UnitOfWork = _works;
            _mapper = mapper;
        }
        public void Create(CategoryBo catagory)
        {
            CategoryEo _Catagory = _mapper.Map<CategoryEo>(catagory);
            _UnitOfWork._categories.Insert(_Catagory);
            _UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            _UnitOfWork._categories.Delete(id);
            _UnitOfWork.Save();
        }

        public IEnumerable<CategoryBo> GetAllItem()
        {
            var _clist = _UnitOfWork._categories.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CategoryEo>, List<CategoryBo>>(_clist);
            return mappedList;
        }


        public CategoryEo GetById(int id)
        {
            CategoryEo item = _UnitOfWork._categories.GetById(id);
           
            return item;
        }

        public void Update(CategoryBo _item)
        {
            var _catagory = _UnitOfWork._categories.GetById(_item.Id);
            _catagory = _mapper.Map(_item, _catagory);
            _UnitOfWork._categories.Update(_catagory);
            _UnitOfWork.Save();
        }
    }
}

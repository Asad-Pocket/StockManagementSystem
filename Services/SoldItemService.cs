using AutoMapper;
using StockManagementSystem.BusinessObjects;
using StockManagementSystem.Models;
using StockManagementSystem.UnitOfWorks;

namespace StockManagementSystem.Services
{
    public class SoldItemService : ISoldItemService
    {
        private readonly IApplicationUnitOfWorks _unitOfWork;

        private IMapper _mapper;
        public SoldItemService(IApplicationUnitOfWorks unitOfwork,IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _mapper = mapper;
        }
        public void Create(List<StockOutRecord> items)
        {
            foreach (var _item in items)
            {
                _unitOfWork._soldItems.Insert(_item);
                _unitOfWork.Save();
            }
        }
        public List<StockOutRecord> GetAll()
        {

            List<StockOutRecord> items = _unitOfWork._soldItems.GetAllItem().ToList();
            return items;

        }

        public List<StockOutRecord> GetItemQuantity(DateTime formattedDate, DateTime formattedDate2)
        {
            List<StockOutRecord> solditems = _unitOfWork._soldItems.SearchByDate(formattedDate, formattedDate2);

            return solditems;
        }
    }
}

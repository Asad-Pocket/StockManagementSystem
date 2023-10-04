using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
using ItemBo = StockManagementSystem.BusinessObject.Item;
using ItemEo = StockManagementSystem.Models.Item;
namespace StockManagementSystem.Controllers
{
    public class StockController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IItemService _itemServices;
        private IMapper _mapper;

        public StockController(IItemService itemServices, ICompanyService companyService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
            _itemServices = itemServices;

        }
        public IActionResult StockIn()
        {
            List<CompanyBo> list = _companyService.GetAllItem().ToList();
            List<ItemEo> itemlist = _itemServices.GetAllItem().ToList();

            CompanyItemViewModel item = new CompanyItemViewModel();
            item.Companies = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(list);
            item.Items = itemlist;
            return View(item);
        }

        [HttpPost]
        public IActionResult StockIn(CompanyItemViewModel item)
        {
            //Update the item's quantity in the database
            _itemServices.AddItemQuantity(item.SelectedItemId, item.StockNewQuantity);

            // Return to the StockIn page
            return View("Confirmation");
        }
        public IActionResult GetItemsByCompanyId(int companyId)
        {
            List<ItemBo> itemBO = _itemServices.GetItemsByCompanyId(companyId);
            IEnumerable<ItemEo> items = _mapper.Map<List<ItemBo>, List<ItemEo>>(itemBO);
            return Json(items);
        }

        public IActionResult UpdateItemDetails(int itemId)
        {
            ItemBo item = _itemServices.GetById(itemId);
            ItemEo item2 = _mapper.Map<ItemEo>(item);
            if (item2 == null)
            {
                return NotFound();
            }
            return Json(item2);
        }
        public IActionResult StockOut()
        {
            List<CompanyBo> list = _companyService.GetAllItem().ToList();
            List<ItemEo> itemlist = _itemServices.GetAllItem().ToList();

            CompanyItemViewModel item = new CompanyItemViewModel();
            item.Companies = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(list);
            item.Items = itemlist;
            return View(item);
        }
        [HttpPost]
        public IActionResult StockOut(CompanyItemViewModel item)
        {
            //Update the item's quantity in the database
            _itemServices.DeleteItemQuantity(item.SelectedItemId, item.StockNewQuantity);

            // Return to the StockIn page
            return View("Confirmation");
        }
    }
}
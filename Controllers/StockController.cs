using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.BusinessObjects;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using StockManagementSystem.Services;
using System.Data;
using System.Globalization;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
using ItemBo = StockManagementSystem.BusinessObject.Item;
using ItemEo = StockManagementSystem.Models.Item;
namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IItemService _itemServices;
        private readonly ISoldItemService _soldItemsService;
        private IMapper _mapper;

        public StockController(IItemService itemServices, ICompanyService companyService,ISoldItemService soldItemService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
            _itemServices = itemServices;
            _soldItemsService = soldItemService;

        }
        [Authorize(Roles = "StockManager,Admin")]
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
        public IActionResult Confirmation()
        {
           
            return View();
        }
        [Authorize(Roles = "StockManager,Admin")]
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
        public IActionResult SellItem([FromBody] StockOutRoot model)
        {
            foreach (var solditem in model.Items)
            {
                solditem.Date = DateTime.Now;
                solditem.StockOutType = model.StockOutType;
            }
            

            _itemServices.DeleteQuantity(model.Items);
            _soldItemsService.Create(model.Items);

            return Json("ok");
        }
        public IActionResult SearchSellItemByDate()
        {
            List<StockOutRecord> items = _soldItemsService.GetAll().ToList();

            var distinctDates = items.Select(item => item.Date.Date).Distinct().ToList();
            ViewData["solditem"] = distinctDates;
            return View();
        }
        [HttpPost]
        public JsonResult SearchItems([FromBody] SearchByDatesModel jsonstring)
        {


            DateTime date1 = DateTime.ParseExact(jsonstring.Date1, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(jsonstring.Date2, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            List<StockOutRecord> items = _soldItemsService.GetItemQuantity(date1, date2);

            return Json(items);
        }
    }
}
    public class StockOutRoot
    {
        public List<StockOutRecord> Items { get; set; }
        public StockOutType StockOutType { get; set; }
    }

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.Service;
using StockManagementSystem.UnitOfWorks;
using System.Diagnostics;
using CategoryBo = StockManagementSystem.BusinessObject.Category;
using CategoryEo = StockManagementSystem.Models.Category;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
using ItemBo = StockManagementSystem.BusinessObject.Item;
using ItemEo = StockManagementSystem.Models.Item;
namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {

        private readonly IItemService _service;
        private readonly IMapper _mapper;
        public ItemController(IItemService repo, IMapper mapper)
        {
            _service = repo;
            _mapper = mapper;
        }
        public IActionResult Create()
        {
            var Temp_category = _service.GetCategory().ToList();
            var mappedcategory = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(Temp_category);
            ViewData["_category"] = mappedcategory;
            var Temp_company = _service.GetCompany().ToList();
            var mappedcompany = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(Temp_company);
            ViewData["_comp"] = mappedcompany;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemEo _item)
        {
            var obj = _mapper.Map<ItemBo>(_item);
            _service.Create(obj);
            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            var _list = _service.GetAllItem();
            return View(_list);
        }
        public IActionResult Update(int id)
        {
            var catlist = _service.GetCategory();
            var mappedcat = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(catlist);
            var comlist = _service.GetCompany();
            var mappedcom = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(comlist);
            ViewData["_cat"] = mappedcat;
            ViewData["_com"] = mappedcom;
            var item = _service.GetById(id);
            var obj = _mapper.Map<ItemEo>(item);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(ItemEo item)
        {
            var obj = _mapper.Map<ItemBo>(item);
            _service.Update(obj);
            return RedirectToAction("Show");
        }
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Show");
        }
        [AllowAnonymous]
        public ActionResult CategoryChart()
        {
            var  categoryCounts = _service.CalculateCategoryCounts();

            ViewBag.CategoryCounts = JsonConvert.SerializeObject(categoryCounts);

            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public IActionResult SearchItem()
        {
            var temp = _service.GetCategory().ToList();
            var categorylist = _mapper.Map<List<CategoryBo>,List<CategoryEo>>(temp);
            var temp2 = _service.GetCompany().ToList();
            var companylist = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(temp2);
            SearchViewItem viewmodel = new SearchViewItem()
            {
                Categories = categorylist,
                Companies = companylist
            };
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult GetItems(int? selectedCompany, int? selectedCategory)
        {
            List<ItemEo> items;

            if (selectedCompany.HasValue && !selectedCategory.HasValue)
            {
                items = _service.GetItemByCompany(selectedCompany.Value).ToList();
            }
            else if (!selectedCompany.HasValue && selectedCategory.HasValue)
            {
                items = _service.GetItemByCategory(selectedCategory.Value).ToList();
            }
            else if (selectedCompany.HasValue && selectedCategory.HasValue)
            {
                items = _service.GetItemByCompanyAndCategory(selectedCompany.Value, selectedCategory.Value).ToList();
            }
            else
            {
                // Handle the case when neither company nor category is selected
                items = new List<ItemEo>();
            }
            return Json(items);
        }
        [HttpPost]
        public IActionResult SearchItem(List<ItemEo> _items)
        {
            return View();
        }
    }
}

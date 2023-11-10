using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using StockManagementSystem.UnitOfWorks;
using System.Text;
using CategoryBo = StockManagementSystem.BusinessObject.Category;
using CategoryEo = StockManagementSystem.Models.Category;
namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService units , IMapper mapper)
        {
            _service = units;
            _mapper = mapper;
        }
        [Authorize(Roles ="StockManager,Admin")]
        public IActionResult Create() 
        {
            var items = _service.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(items);
            ViewData["Category"] = mappedList;
            var temp = User.IsInRole("StockManager");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryEo catagory)
        {
            CategoryBo _Category = _mapper.Map<CategoryBo>(catagory);
            _service.Create(_Category);
            return RedirectToAction("Create");
        }
        public IActionResult Show()
        {
            var it = _service.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(it);
            return View(mappedList);
        }
        [Authorize(Roles = "StockManager,Admin")]
        public IActionResult Update(int id)
        {
            var it = _service.GetById(id);

            return View(it);
        }
        [HttpPost]
        public IActionResult Update(CategoryEo _cat)
        {
            CategoryBo _Catagory = _mapper.Map<CategoryBo>(_cat);
            _service.Update(_Catagory);
            return RedirectToAction("Show");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Show");
        }
        public FileContentResult DownloadCsv()
        { 
            List<CategoryBo> Categories = _service.GetAllItem().ToList(); // Retrieve items from the database here
            var items = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(Categories);

            StringBuilder Csv = new StringBuilder();
            Csv.AppendLine("Id,Type"); // Add headers

            foreach (var item in items)
            {
                Csv.AppendLine($"{item.Id},{item.Type}");
            }

            byte[] data = Encoding.UTF8.GetBytes(Csv.ToString());
            return File(data, "text/csv", "CategoryList.csv");
        }
    }
}

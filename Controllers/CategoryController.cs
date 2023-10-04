using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using StockManagementSystem.UnitOfWorks;
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
        public IActionResult Create() 
        {
            var items = _service.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CategoryBo>, List<CategoryEo>>(items);
            ViewData["Category"] = mappedList;
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
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Show");
        }

    }
}

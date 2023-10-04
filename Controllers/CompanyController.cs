using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using StockManagementSystem.UnitOfWorks;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;

namespace StockManagementSystem.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService repo,IMapper mapper)
        {
            _service = repo;
            _mapper = mapper;
        }
        public IActionResult Create()
        {
            var items = _service.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(items);
            ViewData["_company"] = mappedList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CompanyEo company)
        {
            CompanyBo _company = _mapper.Map<CompanyBo>(company);
            _service.Create(_company);
            return RedirectToAction("Create");
        }
        public IActionResult Update(int id )
        {
            var its = _service.GetById(id);
            return View(its);
        }
        [HttpPost]
        public IActionResult Update(CompanyEo com) 
        {
            CompanyBo _company = _mapper.Map<CompanyBo>(com);
            _service.Update(_company);
            return RedirectToAction("Show");
        }
        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            return RedirectToAction("Show");
        }
        public IActionResult Show()
        {
            var it = _service.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(it);
            return View(mappedList);
        }
    }
}

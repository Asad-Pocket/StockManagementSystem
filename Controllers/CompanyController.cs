using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.Repositories;
using StockManagementSystem.Service;
using StockManagementSystem.UnitOfWorks;
using System.Text;
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
        [Authorize(Roles = "StockManager,Admin")]
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
        [Authorize(Roles = "StockManager,Admin")]
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
        [Authorize(Roles = "Admin")]
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
        public FileContentResult DownloadCsv()
        {
            List<CompanyBo> companies = _service.GetAllItem().ToList(); // Retrieve items from the database here
            var items = _mapper.Map<List<CompanyBo>, List<CompanyEo>>(companies);

            StringBuilder Csv = new StringBuilder();
            Csv.AppendLine("Id,Name"); // Add headers

            foreach (var item in items)
            {
                Csv.AppendLine($"{item.Id},{item.Name}");
            }

            byte[] data = Encoding.UTF8.GetBytes(Csv.ToString());
            return File(data, "text/csv", "CategoryList.csv");
        }
    }
}

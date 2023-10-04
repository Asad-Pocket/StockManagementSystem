using Microsoft.AspNetCore.Mvc;

namespace StockManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

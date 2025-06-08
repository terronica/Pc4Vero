using Microsoft.AspNetCore.Mvc;

namespace EcommercePC4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

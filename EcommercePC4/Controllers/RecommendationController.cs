using EcommercePC4.MLModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePC4.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly RecommendationService _service;

        public RecommendationController(RecommendationService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Recommend(string userId)
        {
            if (!float.TryParse(userId, out float uid))
            {
                ModelState.AddModelError("", "UserId debe ser numérico.");
                return View("Index");
            }

            var recommendations = _service.RecommendProducts(uid);
            ViewBag.Recommendations = recommendations;
            ViewBag.UserId = userId;
            return View("Index");
        }
    }
}

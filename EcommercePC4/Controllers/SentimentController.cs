using EcommercePC4.MLModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePC4.Controllers
{
    public class SentimentController : Controller
    {
        private readonly SentimentService _service;

        public SentimentController()
        {
            _service = new SentimentService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Analyze(string userText)
        {
            var result = _service.Predict(userText);
            ViewBag.Prediction = result.Prediction ? "Positivo" : "Negativo";
            ViewBag.Score = result.Probability.ToString("P2");
            return View("Index");
        }
    }
}

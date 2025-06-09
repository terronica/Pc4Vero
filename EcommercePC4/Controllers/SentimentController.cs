using EcommercePC4.MLModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePC4.Controllers
{
    public class SentimentController : Controller
    {
        private readonly SentimentService _sentimentService;

        public SentimentController(SentimentService sentimentService)
        {
            _sentimentService = sentimentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpPost]
        // public IActionResult Analyze(string userText)
        // {
        //     var result = _service.Predict(userText);
        //     ViewBag.Prediction = result.Prediction ? "Positivo" : "Negativo";
        //     ViewBag.Score = result.Probability.ToString("P2");
        //     return View("Index");
        // }

        [HttpPost]
        public IActionResult Predict(string inputText)
        {
            var result = _sentimentService.Predict(inputText);
            return View(result);
        }
    }
}

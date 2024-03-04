using Microsoft.AspNetCore.Mvc;
using Rabbit.Producer.Models;
using System.Diagnostics;
using Web.Models;

namespace Rabbit.Producer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRabbitService _rabbitService;

        public HomeController(ILogger<HomeController> logger, IRabbitService rabbitService)
        {
            _logger = logger;
            _rabbitService = rabbitService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Category category)
        {
            _rabbitService.PostMessage(category);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

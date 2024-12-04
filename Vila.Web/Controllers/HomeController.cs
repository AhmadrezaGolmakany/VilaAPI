using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vila.Web.Models;
using Vila.Web.Services.Vila;

namespace Vila.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaService _vilaService;

        public HomeController(IVilaService vilaService)
        {
            _vilaService = vilaService;
        }

        public async Task<IActionResult> Index(int pageId =1 , string fillter="" , int take =6)
        {
            var model = await _vilaService.Search(pageId,fillter,take);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

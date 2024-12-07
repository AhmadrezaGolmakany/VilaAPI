using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vila.Web.Models;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Vila;

namespace Vila.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVilaService _vilaService;
        private readonly IAuthService _authService;

        public HomeController(IVilaService vilaService , IAuthService authService)
        {
            _vilaService = vilaService;
            _authService = authService;
        }

        [Authorize]
        public async Task<IActionResult> Index(int pageId =1 , string filter = "" , int take =6)
        {
            //string token = HttpContext.Session.GetString("JWTsecret");

            string token = _authService.GetJwtToken();


            var model = await _vilaService.Search(pageId, filter, take,token);
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

using Microsoft.AspNetCore.Mvc;
using Vila.Web.Models.customer;
using Vila.Web.Services.Customer;

namespace Vila.Web.Controllers
{
    public class AccountController : Controller
    {
        public readonly ICustomerService _customerservice;

        public AccountController(ICustomerService customerservice)
        {
            _customerservice = customerservice;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var res = await _customerservice.Register(model);

            if (res.Result)
            {
                TempData["success"] = true;
                return View();
            }
            else 
            {
                ModelState.AddModelError("", "شماره موبایل تکراری است.");
                return View(model);
            }



        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var res = await _customerservice.Login(model);

            if (!res.Result.Result)
            {
                ModelState.AddModelError("",res.Result.Message);
                return View(model);
            }
            var customer = res.customer;
            HttpContext.Session.SetString("JWTsecret" ,customer.JwtSecret );

            return Redirect("/");



        }
    }
}

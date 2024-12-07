using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vila.Web.Models.customer;
using Vila.Web.Services.Customer;

namespace Vila.Web.Controllers
{
    public class AccountController : Controller
    {
        public readonly ICustomerService _customerservice;
        public readonly IAuthService _authService;

        public AccountController(ICustomerService customerservice , IAuthService authService)
        {
            _customerservice = customerservice;
            _authService = authService;
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

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, customer.Mobile));
            identity.AddClaim(new Claim(ClaimTypes.Role, customer.Role));
            identity.AddClaim(new Claim("JWTsecret", customer.JwtSecret));

            var principal = new ClaimsPrincipal(identity);



            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme ,principal );

            //HttpContext.Session.SetString("JWTsecret" ,customer.JwtSecret );

            return Redirect("/");



        }



        public IActionResult LogOut()
        {
            _authService.SignOut();
            return RedirectToAction("Login");
        }

        public IActionResult NotAccess()
        {
            return View();
        }
    }
}

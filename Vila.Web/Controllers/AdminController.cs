using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Common;
using Vila.Web.Models.Vila;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Vila;
using Vila.Web.Utility;

namespace Vila.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IVilaService _vilaService;
        private readonly ApiUrls _urls;
        private readonly IAuthService _authservice;

        public AdminController(IVilaService vilaService , IOptions<ApiUrls> urls, IAuthService authservice)
        {
            _vilaService = vilaService;
            _urls = urls.Value;
            _authservice = authservice;
        }

        public async Task<IActionResult> GetAllVila(int pageId = 1, string filter = "", int take = 6)
        {

            var secret =  _authservice.GetJwtToken();

            var url = _urls.BaseAddress + _urls.VilaV1Address;

            var model = await _vilaService.Search(pageId, filter, take, secret);


            return View(model);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VilaModel vila , IFormFile picture)
        {

            if (!ModelState.IsValid) return View(vila);


            try
            {
                DateTime date = vila.MadeDate.ToEnglishDateTime();
            }
            catch 
            {
                ModelState.AddModelError("MadeDate", "فرمت تاریخ باید 1379/12/08 باشد");
                return View(vila);

            }

            if (picture == null || !picture.IsImage())
            {
                ModelState.AddModelError("Image", "لطفا عکس با فرمت .jpg وارد کنید");
                return View(vila);
            }


            ///convert picture to byte array
            using (var open = picture.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                open.CopyTo(ms);
                vila.Image = ms.ToArray();
            }





            var secret = _authservice.GetJwtToken();

            var url = _urls.BaseAddress + _urls.VilaV1Address;

            bool created = await _vilaService.Create(url,secret , vila);


            if (created)
            {
                TempData["success"] = true;

            }

            return RedirectToAction("GetAllVila");

        }


        public async Task<IActionResult>  EditeVila(int id)
        {
            var secret = _authservice.GetJwtToken();

            var url = $"{_urls.BaseAddress}{_urls.VilaV1Address}/GetDetails/{id}";


            var vila = await _vilaService.GetById(url,secret);   

            return View(vila);
        }

        [HttpPost]
        public async Task<IActionResult> EditeVila(int id , VilaModel vila , IFormFile picture)
        {
            if (!ModelState.IsValid) return View(vila);

            if (picture != null )
            {
                if (!picture.IsImage())
                {
                    ModelState.AddModelError("Image", "لطفا عکس با فرمت .jpg وارد کنید");
                    return View(vila);

                }
                using (var open = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    open.CopyTo(ms);
                    vila.Image = ms.ToArray();
                }

            }

            var secret = _authservice.GetJwtToken();

            var url = $"{_urls.BaseAddress}{_urls.VilaV1Address}/Update/{id}";

            bool update = await _vilaService.UpDate(url , secret , vila);


            if (update)
            {
                TempData["success"] = true;

            }

            return RedirectToAction("GetAllVila");


        }


        public async Task<IActionResult> DeleteVila(int id)
        {

            var secret = _authservice.GetJwtToken();

            var url = $"{_urls.BaseAddress}{_urls.VilaV1Address}/{id}";

            bool delete = await _vilaService.Delete(url , secret);


            if (delete)
            {
                TempData["success"] = true;

            }

            return RedirectToAction("GetAllVila");
        }
    }
}

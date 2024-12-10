using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;
using NuGet.Common;
using System.Reflection;
using Vila.Web.Models.Detail;
using Vila.Web.Models.Vila;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Detail;
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
        private readonly IDetailService _detail;

        public AdminController(IVilaService vilaService, IOptions<ApiUrls> urls, IAuthService authservice, IDetailService detail)
        {
            _vilaService = vilaService;
            _urls = urls.Value;
            _authservice = authservice;
            _detail = detail;
        }

        #region VilaAdmin
        public async Task<IActionResult> GetAllVila(int pageId = 1, string filter = "", int take = 6)
        {

            var secret = _authservice.GetJwtToken();

            var url = _urls.BaseAddress + _urls.VilaV1Address;

            var model = await _vilaService.Search(pageId, filter, take, secret);


            return View(model);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VilaModel vila, IFormFile picture)
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

            bool created = await _vilaService.Create(url, secret, vila);


            if (created)
            {
                TempData["success"] = true;

            }

            return RedirectToAction("GetAllVila");

        }


        public async Task<IActionResult> EditeVila(int id)
        {
            var secret = _authservice.GetJwtToken();

            var url = $"{_urls.BaseAddress}{_urls.VilaV1Address}/GetDetails/{id}";


            var vila = await _vilaService.GetById(url, secret);

            return View(vila);
        }

        [HttpPost]
        public async Task<IActionResult> EditeVila(int id, VilaModel vila, IFormFile picture)
        {
            if (!ModelState.IsValid) return View(vila);

            if (picture != null)
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

            bool update = await _vilaService.UpDate(url, secret, vila);


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

            bool delete = await _vilaService.Delete(url, secret);


            if (delete)
            {
                TempData["success"] = true;

            }

            return RedirectToAction("GetAllVila");
        }
        #endregion



        #region DetailAdmin


        public async Task<IActionResult> GetDetailVila(int id)
        {
            var token = _authservice.GetJwtToken();

            var urlVila = $"{_urls.BaseAddress}{_urls.VilaV1Address}/GetDetails/{id}";
            var vila = await _vilaService.GetById(urlVila, token);

            if (vila == null) return NotFound();





            var url = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}/GetAllVilaDetail/{id}";

            var model = await _detail.GetAll(url, token);
            ViewData["vila"] = vila;


            return View(model);
        }



        public IActionResult CreateVilaDetail(int id)
        {
            DetailModel model = new DetailModel() { VilaId = id };

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> CreateVilaDetail(int id, DetailModel model)
        {
            if (id != model.VilaId) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var token = _authservice.GetJwtToken();
            var url = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}";

            bool create = await _detail.Create(url, token, model);


            if (create)
                TempData["success"] = true;

            return Redirect($"/Admin/GetDetailVila/{id}");
        }




        public async Task<IActionResult> EditVilaDetail(int id)
        {
            var token = _authservice.GetJwtToken();
            var url = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}/GetById/{id}";

            var model = await _detail.GetById(url, token);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVilaDetail(int id, DetailModel model)
        {
            if (model.DeyailaId != id) return BadRequest();

            if (!ModelState.IsValid) return View(model);


            var token = _authservice.GetJwtToken();
            var url = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}/Update/{id}";


            bool update = await _detail.UpDate(url, token, model);


            if (update)
                TempData["success"] = true;

            return Redirect($"/Admin/GetDetailVila/{model.VilaId}");
        }



        public async Task<IActionResult> DeleteVilaDetail(int id)
        {
            var token = _authservice.GetJwtToken();

            var urlget = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}/GetById/{id}";

            var model = await _detail.GetById(urlget, token);
            var url = $"{_urls.BaseAddress}{_urls.VilaDetailAddress}/{id}";



            bool delete = await _detail.Delete(url, token);


            if (delete)
                TempData["success"] = true;

            return Redirect($"/Admin/GetDetailVila/{model.VilaId}");
        }
        #endregion
    }
}

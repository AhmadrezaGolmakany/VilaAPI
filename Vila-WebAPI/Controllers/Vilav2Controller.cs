using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Paging;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Vila")]
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]

    public class VilaV2Controller : ControllerBase
    {
        private readonly IVilaServices _vilaServices;

        public VilaV2Controller(IVilaServices vilaServices)
        {
            _vilaServices = vilaServices;
        }
        /// <summary>
        /// فیلترینگ ویلا یا جستجو
        /// </summary>
        /// <param name="pageid">ایدی صفحه</param>
        /// <param name="filter">متن جستجو</param>
        /// <param name="take">تعداد نمایش</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(VilaPaging))]
        [ProducesResponseType(400)]
        public IActionResult Serch(int pageid =1 , string filter="" , int take=1)
        {
            if (pageid < 1 || take < 1) return BadRequest();  
            var model = _vilaServices.SearchVila(pageid, filter, take);
            return Ok(model);
        }
    }
}

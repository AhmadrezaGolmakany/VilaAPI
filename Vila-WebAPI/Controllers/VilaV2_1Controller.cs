using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Paging;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Vila")]
    [ApiController]
    [ApiVersion("2.1")]
    [Authorize(Roles = "admin")]
    public class VilaV2_1Controller : ControllerBase
    {
        private readonly IVilaServices _vilaServices;

        public VilaV2_1Controller(IVilaServices vilaServices)
        {
            _vilaServices = vilaServices;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(VilaPagingAdmin))]
        [ProducesResponseType(400)]

        public IActionResult Serch(int pageid = 1, string filter = "", int take = 1)
        {
            if (pageid < 1 || take < 1) return BadRequest();
            var model = _vilaServices.SearchVilaAdmin(pageid, filter, take);
            return Ok(model);
        }
    }
}

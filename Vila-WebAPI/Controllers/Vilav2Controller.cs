using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.Intefaces;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/vila")]
    [ApiController]
    [ApiVersion("2.0")]

    public class VilaV2Controller : ControllerBase
    {
        private readonly IVilaServices _vilaServices;

        public VilaV2Controller(IVilaServices vilaServices)
        {
            _vilaServices = vilaServices;
        }

        [HttpGet]
        public IActionResult Serch(int pageid =1 , string filter="" , int take=1)
        {
            if (pageid < 1 || take < 1) return BadRequest();  
            var model = _vilaServices.SearchVila(pageid, filter, take);
            return Ok(model);
        }
    }
}

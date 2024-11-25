using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Controllers
{
    [Route("api/Vila")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IVilaServices _vila;

        private readonly IMapper _mapper;
        public VilaController(IVilaServices vila , IMapper mapper)
        {
            _vila = vila;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _vila.GetAllVilas();
            List<VilaDTOs> model = new();

            list.ForEach(x =>
            {
                model.Add(_mapper.Map<VilaDTOs>(x));
            });


            return Ok(model);
        }

        [HttpGet("[action]/{id:int}" , Name = "GetDetails")]
        public IActionResult GetDetails(int id) 
        {
            var vila = _vila.GetById(id);
            if (vila == null) { return NotFound(); }

            var model = _mapper.Map<VilaDTOs>(vila);

            return Ok(model);

        }

        [HttpPost]
        public IActionResult CreateVila([FromBody] VilaDTOs model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vila = _mapper.Map<Vila>(model);
            

            if (!_vila.AddVila(vila)) 
            {
                return CreatedAtRoute("GetDetails" , new {id = vila.VilaID} , _mapper.Map<VilaDTOs>(vila));

            }
            ModelState.AddModelError("","مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500,ModelState);

        }

        [HttpPatch("[action]/{vilaid:int}")]
        public IActionResult Update([FromBody] VilaDTOs model , int vilaid)
        {
            if (vilaid != model.VilaID) 
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vila = _mapper.Map<Vila>(model);
           

            if (!_vila.UpdateVila(vila))
            {
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);

        }



        [HttpDelete("{vilaid:int}")]
        public IActionResult Remove(int vilaid)
        {
            var vila = _vila.GetById(vilaid);

            if (vila == null)
            {
                return NotFound();
            }

            if (!_vila.RemoveVila(vila))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);
        }
    }
}

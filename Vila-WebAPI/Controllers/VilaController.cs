using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Vila")]
    //[Route("api/Vila")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VilaController : ControllerBase
    {
        private readonly IVilaServices _vila;

        private readonly IMapper _mapper;
        public VilaController(IVilaServices vila , IMapper mapper)
        {
            _vila = vila;
            _mapper = mapper;
        }


        /// <summary>
        /// برای گرفتن تمامی ویلاها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(502)]
        [Authorize(Roles = "admin")]

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



        /// <summary>
        /// برای گرفتن اطلاعات ویلا تکی
        /// </summary>
        /// <param name="id">ایدی ویلا</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]
        [Authorize]

        [HttpGet("[action]/{id:int}" , Name = "GetDetails")]
        public IActionResult GetDetails(int id) 
        {
            var vila = _vila.GetById(id);
            if (vila == null) { return NotFound(); }

            var model = _mapper.Map<VilaDTOs>(vila);

            return Ok(model);

        }





        
        /// <summary>
        /// برای اضافه کردن یک ویلا
        /// </summary>
        /// <param name="model">مدل ورودی جهت اضافه کردن</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "admin")]

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




      


        /// <summary>
        /// ویرایش یک ویلا
        /// </summary>
        /// <param name="model">مدل ورودی جهت ویرایش ویلا</param>
        /// <param name="vilaid">ایدی ویلا</param>
        /// <returns></returns>
        [HttpPatch("[action]/{vilaid:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]
        [Authorize(Roles = "admin")]

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
                return NoContent();
            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);

        }






        
        /// <summary>
        /// حذف کردن یک ویلا 
        /// </summary>
        /// <param name="vilaid">ایدی ویلا</param>
        /// <returns></returns>
        [HttpDelete("{vilaid:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]
        [Authorize(Roles = "admin")]

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

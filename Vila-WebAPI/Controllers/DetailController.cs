using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = "admin")]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detailService;
        private readonly IMapper _mapper;
        private readonly IVilaServices _vilaServices;

        public DetailController(IDetailService detailService , IMapper mapper , IVilaServices vila)
        {
            _detailService = detailService;
            _mapper = mapper;
            _vilaServices = vila;
        }


        /// <summary>
        /// برای گرفتن امکانات ویلا
        /// </summary>
        /// <param name="vilaid"> ایدی ویلا</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(502)]
        [ProducesResponseType(404)]
        [HttpGet("[action]/{vilaid:int}")]
        public IActionResult GetAllVilaDetail(int vilaid) 
        {
            var vila = _vilaServices.GetById(vilaid);

            if (vila == null)
            {
                return NotFound();
            }

            var details = _detailService.GetAllVilaDetails(vilaid);

            List<DetailDTO> model = new List<DetailDTO>();
            details.ForEach(x =>
            {
                model.Add(_mapper.Map<DetailDTO>(x));
            });
            return Ok(model);
        }



        

        /// <summary>
        /// گرفتن امکانات ویلا از طریق ایدی
        /// </summary>
        /// <param name="detailid">ایدی</param>
        /// <returns></returns>
        [HttpGet("[action]/{detailid:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(502)]
        public IActionResult GetById(int detailid)
        {
            var detail = _detailService.GetById(detailid);

            if(detail == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<DetailDTO>(detail);

            return Ok(model);
        }



        

        /// <summary>
        /// اضافه کردن امکانات جدید برای یک ویلا
        /// </summary>
        /// <param name="model">مدل ورودی جهت اضافه کردن</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(502)]
        [ProducesResponseType(501)]
        public IActionResult Create([FromBody] DetailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _mapper.Map<Detail>(model);


            if (_detailService.Create(detail))
            {
                var dtoDetail = _mapper.Map<DetailDTO>(detail);

                return CreatedAtRoute("GetDetails", new { id = detail.DeyailaId }, dtoDetail);

            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);

        }



        
        /// <summary>
        /// ویرایش امکانات یک ویلا
        /// </summary>
        /// <param name="model">مدل ورودی</param>
        /// <param name="detailid">ایدی</param>
        /// <returns></returns>
        [HttpPatch("[action]/{detailid:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]

        public IActionResult Update([FromBody] DetailDTO model, int detailid)
        {
            if (detailid != model.DeyailaId)
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detail = _mapper.Map<Detail>(model);


            if (_detailService.Update(detail))
            {
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);

        }




        
        /// <summary>
        /// حذف کردن امکانات یک ویلا
        /// </summary>
        /// <param name="detailid">ایدی</param>
        /// <returns></returns>
        [HttpDelete("{detailid:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(502)]
        public IActionResult Remove(int detailid)
        {
            var detail = _detailService.GetById(detailid);

            if (detail == null)
            {
                return NotFound();
            }

            if (_detailService.Delete(detail))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "مشکلی از سمت سرور پیش آمره لطفا مجددا تلاش کنید.");

            return StatusCode(500, ModelState);
        }

    }
}

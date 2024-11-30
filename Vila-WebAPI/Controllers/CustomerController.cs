using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila_WebAPI.CustomerModels;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;

namespace Vila_WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// ثبت نام
        /// </summary>
        /// <param name="model">موبایل و کلمه عبور</param>
        /// <returns></returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Register")]

        public IActionResult Register([FromBody] RegisterModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(model);
            }

            if (_customerService.ExistMobile(model.Mobile))
            {
                ModelState.AddModelError("model.Mobil", "تلفن همراه وارد شده قبلا در سایت ثبت نام کرده است.");
                return BadRequest(model);

            }

            if (_customerService.Register(model))
            {
                return StatusCode(201);
            }

            ModelState.AddModelError("","خطایی از سمت سرور پیش امده لطفا مجددا تلاش کنید.");
            return StatusCode(500 , ModelState);
        }


        /// <summary>
        /// ورود
        /// </summary>
        /// <param name="model">موبایل و کلمه عبور</param>
        /// <returns></returns>
        [ProducesResponseType(200 , Type = typeof(LoginResultDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            if (_customerService.PasswordIsCorrect(model.Mobile , model.Mobile))
            {
                ModelState.AddModelError("", "کاربری یافت نشد");
                return BadRequest(model);

            }

            var user = _customerService.Login(model.Mobile , model.Pass);

            if (user == null)
            {
                return NotFound();
            }   
            return Ok(user);

        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.customer
{
    public class RegisterModel
    {
        [Display(Name ="موبایل")]
        [Required(ErrorMessage ="موبایل اجاری است")]
        [MinLength(11,ErrorMessage ="موبایل باید 11 رقم باشد")]
        [MaxLength(11,ErrorMessage ="موبایل باید 11 رقم باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور اجباری است")]
        public string Pass { get; set; }
    }
}

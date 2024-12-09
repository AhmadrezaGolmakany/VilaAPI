using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.Vila
{
    public class VilaModel
    {


        public int VilaID { get; set; }

        [Display(Name = "نام ویلا")]
        [Required(ErrorMessage = "{0}اجباری است.")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "{0}اجباری است.")]
        [MaxLength(255)]
        public string? State { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "{0}اجباری است.")]
        [MaxLength(255)]
        public string? City { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "{0}اجباری است.")]
        [MaxLength(255)]
        public string? Address { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "{0}اجباری است.")]
        [MaxLength(11)]
        [MinLength(11)]
        public string? Mobile { get; set; }

        [Display(Name = "قیمت اجاره روزانه")]
        public long dayPrice { get; set; }

        [Display(Name = "عکس")]
        public byte[]? Image { get; set; }

        [Display(Name = "قیمت فروش")]
        public long SellPrice { get; set; }

        [Display(Name = "تاریخ ساخت")]
        [Required(ErrorMessage = "{0}اجباری است. (فرمت 1378/04/04)")]
        public string MadeDate { get; set; }
    }
}

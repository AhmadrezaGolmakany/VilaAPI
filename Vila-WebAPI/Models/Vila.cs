using System.ComponentModel.DataAnnotations;

namespace Vila_WebAPI.Models
{
    public class Vila
    {
        [Key]
        public int VilaID { get; set; }

        [MaxLength(150)]
        [Display(Name = "نام ویلا")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string State { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(150)]
        [Display(Name = "شهر")]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(150)]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(11)]
        [Display(Name = "َتلفن همراه")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [Display(Name = "تاریخ ساخت")]
        public DateTime MadeDate { get; set; }


    }
}

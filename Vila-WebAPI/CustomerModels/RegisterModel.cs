using System.ComponentModel.DataAnnotations;

namespace Vila_WebAPI.CustomerModels
{
    public class RegisterModel
    {
        [MaxLength(11)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Mobile { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Pass { get; set; }
    }
}

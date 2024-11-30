using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vila_WebAPI.Models
{
    public class Customer
    {

        [Key]
        public int userId { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Mobile { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Pass { get; set; }
        [NotMapped]
        public string JwtSecret { get; set; }
        [MaxLength(225)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Role { get; set; }
    }
}

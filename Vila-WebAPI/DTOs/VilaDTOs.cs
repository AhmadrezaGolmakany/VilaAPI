using System.ComponentModel.DataAnnotations;
using Vila_WebAPI.ModelValidation;

namespace Vila_WebAPI.DTOs
{
    public class VilaDTOs
    {
        public int VilaID { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string? Name { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string? State { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string? City { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(150)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(11)]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [DateValidation]
        public string MadeDate { get; set; }
    }
}

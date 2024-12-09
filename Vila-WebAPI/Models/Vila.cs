using System.ComponentModel.DataAnnotations;

namespace Vila_WebAPI.Models
{
    public class Vila
    {
        [Key]
        public int VilaID { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string State { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(150)]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(150)]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [MaxLength(11)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public DateTime MadeDate { get; set; }

        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public long dayPrice { get; set; }

        public long SellPrice { get; set; }

        public List<Detail> details { get; set; }
    }
}

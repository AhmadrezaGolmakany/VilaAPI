using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vila_WebAPI.Models
{
    public class Detail
    {
        [Key]
        public int DeyailaId { get; set; }
        [Required]
        public int VilaId { get; set; }
        [Required]
        [MaxLength(225)]
        public string What { get; set; }
        [Required]
        [MaxLength(500)]
        public string Value { get; set; }

        [ForeignKey("VilaId")]
        public Vila vila { get; set; }
    }
}

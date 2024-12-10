using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.Detail
{
    public class DetailModel
    {

        public int DeyailaId { get; set; }

        [Required]
        [Display(Name = "ایدی ویلا")]
        public int VilaId { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "ویژگی ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "ویژگی ویلا نباید بیش از 255 حرف باشد")]
        public string What { get; set; }



        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "ویژگی ویلا اجباری است")]
        [MaxLength(255, ErrorMessage = "ویژگی ویلا نباید بیش از 255 حرف باشد")]
        public string Value { get; set; }
    }
}
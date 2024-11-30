using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vila_WebAPI.DTOs
{
    public class LoginResultDTO
    {
        public int userId { get; set; }

        
        /// <summary>
        /// توکن احراز هویت Bearer
        /// </summary>
        public string JwtSecret { get; set; }
        
        public string Role { get; set; }
    }
}

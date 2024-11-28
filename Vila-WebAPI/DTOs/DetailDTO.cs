using System.ComponentModel.DataAnnotations;

namespace Vila_WebAPI.DTOs
{
    public class DetailDTO
    {
        public int DeyailaId { get; set; }
        
        public int VilaId { get; set; }
        
        public string What { get; set; }
        
        public string Value { get; set; }
    }
}

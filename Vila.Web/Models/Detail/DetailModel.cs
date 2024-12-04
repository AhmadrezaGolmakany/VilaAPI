using System.ComponentModel.DataAnnotations;

namespace Vila.Web.Models.Detail
{
    public class DetailModel
    {
        public int DeyailaId { get; set; }

        
        public int VilaId { get; set; }

        public string What { get; set; }

        public string Value { get; set; }
    }
}
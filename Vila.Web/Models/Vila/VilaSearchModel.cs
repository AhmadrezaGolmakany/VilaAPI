using Vila.Web.Models.Detail;

namespace Vila.Web.Models.Vila
{
    public class VilaSearchModel
    {
        public int VilaID { get; set; }


        public string? Name { get; set; }


        public string? State { get; set; }


        public string? City { get; set; }


        public string? Address { get; set; }


        public string? Mobile { get; set; }

        public long dayPrice { get; set; }

        public long SellPrice { get; set; }

        public string MadeDate { get; set; }

        public List<DetailModel> details { get; set; }
    }
}
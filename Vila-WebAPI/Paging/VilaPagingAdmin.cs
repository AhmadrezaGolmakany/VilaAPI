using Vila_WebAPI.DTOs;

namespace Vila_WebAPI.Paging
{
    public class VilaPagingAdmin : BasePaging
    {
        public List<VilaDTOs> vilaDTO { get; set; }
        public string Filter { get; set; }

    }
}

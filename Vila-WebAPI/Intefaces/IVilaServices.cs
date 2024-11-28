using Vila_WebAPI.Models;
using Vila_WebAPI.Paging;

namespace Vila_WebAPI.Intefaces
{
    public interface IVilaServices
    {
        List<Vila> GetAllVilas();

        bool AddVila(Vila vila);
        bool RemoveVila(Vila vila);
        bool UpdateVila(Vila vila);

        Vila GetById(int id);
        bool Save();


        VilaPaging SearchVila(int pageid , string filter , int take);
    }
}

using Vila_WebAPI.Models;

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
    }
}

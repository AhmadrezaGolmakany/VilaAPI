using Vila_WebAPI.Context;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Services
{
    public class VilaServices : IVilaServices
    {
        private readonly VilaContext _context;

        public VilaServices(VilaContext context)
        {
            _context = context;
        }

        public bool AddVila(Vila vila)
        {

            _context.Add(vila);
            return Save();


        }


        public List<Vila> GetAllVilas()
        {
            return _context.vilas.ToList();
        }

        public Vila GetById(int id)
        {
            return _context.vilas.FirstOrDefault(v=> v.VilaID == id);
        }

        public bool RemoveVila(Vila vila)
        {

            _context.vilas.Remove(vila);
            return Save();


        }

        public bool Save() =>
            _context.SaveChanges() <= 0 ? true : false;


        public bool UpdateVila(Vila vila)
        {
            _context.vilas.Update(vila);
            return Save();
        }
    }
}

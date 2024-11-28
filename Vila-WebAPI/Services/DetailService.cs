using Vila_WebAPI.Context;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Services
{
    public class DetailService : IDetailService
    {
        private readonly VilaContext _context;
        public DetailService(VilaContext context)
        {
            _context = context;
        }
        public bool Create(Models.Detail model)
        {




            _context.details.Add(model);

            return Save();


        }

        public bool Delete(Models.Detail model)
        {
            _context.details.Remove(model);
            return Save();
        }

        public List<Models.Detail> GetAllVilaDetails(int vilaId) =>
            _context.details.Where(D => D.VilaId == vilaId).ToList();

        public Models.Detail GetById(int id) =>
            _context.details.FirstOrDefault(v => v.DeyailaId == id);

        public bool Save() =>
            _context.SaveChanges() >= 0 ? true : false;

        public bool Update(Models.Detail model)
        {
            _context.details.Update(model);
            return Save();
        }
    }
}

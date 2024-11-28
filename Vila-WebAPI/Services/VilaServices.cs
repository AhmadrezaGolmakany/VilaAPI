using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vila_WebAPI.Context;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;
using Vila_WebAPI.Paging;

namespace Vila_WebAPI.Services
{
    public class VilaServices : IVilaServices
    {
        private readonly VilaContext _context;
        private readonly IMapper _mapper;

        public VilaServices(VilaContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public VilaPaging SearchVila(int pageid, string filter, int take)
        {
            IQueryable<Vila> result = _context.vilas.Include(v => v.details);
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(r => r.Name.Contains(filter) ||
                    r.State.Contains(filter) ||
                    r.City.Contains(filter) ||
                    r.Address.Contains(filter) 
                );
            }

            VilaPaging paging = new();
            paging.Generate(result , pageid , take);
            paging.Filter = filter;
            paging.vilas = new();
            int skip = (pageid - 1) *take ;

            var list = result.Skip(skip).Take(take).ToList();
            list.ForEach(v =>
            {
                paging.vilas.Add(_mapper.Map<VilaSearch>(v));
            });
            return paging;
        }

        public bool UpdateVila(Vila vila)
        {
            _context.vilas.Update(vila);
            return Save();
        }
    }
}

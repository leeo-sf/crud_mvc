using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Service
{
    public class GeneroService
    {
        private readonly Context _context;

        public GeneroService(Context context)
        {
            _context = context;
        }

        public async Task<List<Genero>> ToList()
        {
            return await _context.Genero.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}

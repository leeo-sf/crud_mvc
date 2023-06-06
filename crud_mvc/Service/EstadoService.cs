using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Service
{
    public class EstadoService
    {
        private readonly Context _context;

        public EstadoService(Context context)
        {
            _context = context;
        }

        public async Task<List<Estado>> ToList()
        {
            return await _context.Estado.ToListAsync();
        }
    }
}

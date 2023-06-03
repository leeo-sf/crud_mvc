using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Service
{
    public class ProfissaoService
    {
        private readonly Context _context;

        public ProfissaoService(Context context)
        {
            _context = context;
        }

        public async Task<List<Profissao>> ToList()
        {
            return await _context.Profissao.ToListAsync();
        }

        public async Task Create(Profissao obj)
        {
            _context.Profissao.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}

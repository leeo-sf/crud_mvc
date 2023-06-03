using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Service
{
    public class PessoaService
    {
        private readonly Context _context;

        public PessoaService(Context contexto)
        {
            _context = contexto;
        }

        public async Task<List<Pessoa>> ToList()
        {
            return await _context.Pessoa.ToListAsync();
        }
    }
}

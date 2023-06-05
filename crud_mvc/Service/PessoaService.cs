using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;
using crud_mvc.Service.Exceptions;

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
            return await _context.Pessoa.OrderBy(x => x.Nome).ToListAsync(); ;
        }

        public async Task Create(Pessoa obj)
        {
            _context.Pessoa.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Pessoa> FindById(int id)
        {
            return await _context.Pessoa.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task Edit(Pessoa obj)
        {
            bool idExiste = await _context.Pessoa.AnyAsync(x => x.Id == obj.Id);
            if (!idExiste)
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

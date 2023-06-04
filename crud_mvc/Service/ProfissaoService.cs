using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;
using crud_mvc.Service.Exceptions;

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

        public async Task<Profissao> FindById(int id)
        {
            return await _context.Profissao.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Profissao obj)
        {
            // retorna true ou false se o id existe
            var idExite = _context.Profissao.Any(x => x.Id == obj.Id);

            if (!idExite)
            {
                throw new Exception("Id não existe");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var obj = await _context.Profissao.FindAsync(id);
                _context.Profissao.Remove(obj);
                await _context.SaveChangesAsync();
            } // irá cair no catch se alguma pessoa tiver com a profissão que estamos tentando apagar
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não é possível apagar a profissão, pois, tem pessoa (s) que dependem dessa profissão");
            }
        }
    }
}

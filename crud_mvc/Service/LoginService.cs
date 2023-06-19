using crud_mvc.Data;
using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Service
{
    public class LoginService
    {
        private readonly Context _context;

        public LoginService(Context context)
        {
            _context = context;
        }

        public async Task<Usuario> CheckLogin(Usuario obj)
        {
            bool email = await _context.Usuario.AnyAsync(x => x.Email == obj.Email);
            bool senha = await _context.Usuario.AnyAsync(x => x.Senha == obj.Senha);
            if (email && senha)
            {
                return await _context.Usuario.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Email == obj.Email);
            }
            return null;
        }

        public async Task<Usuario> FindByEmail(string email)
        {
            return await _context.Usuario.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

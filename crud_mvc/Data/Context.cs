using crud_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Profissao> Profissao { get; set; }
    }
}

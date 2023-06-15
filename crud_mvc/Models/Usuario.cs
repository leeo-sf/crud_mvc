using System.ComponentModel.DataAnnotations.Schema;

namespace crud_mvc.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Column("id_categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}

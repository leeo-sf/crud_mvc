namespace crud_mvc.Models
{
    public class Profissao
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Profissao() { }

        public Profissao(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}

namespace crud_mvc.Models.ViewModels
{
    public class PessoaFormView
    {
        public Pessoa Pessoa { get; set; }
        public ICollection<Genero> Genero { get; set; }
        public ICollection<Profissao> Profissao { get; set; }
        public ICollection<Estado> Estado { get; set; }
    }
}

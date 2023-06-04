namespace crud_mvc.Models.ViewModels
{
    public class PessoaFormView
    {
        public Pessoa Pessoa { get; set; }
        public ICollection<Profissao> Profissao { get; set; }
    }
}

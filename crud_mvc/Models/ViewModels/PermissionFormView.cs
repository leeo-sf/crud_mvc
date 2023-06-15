namespace crud_mvc.Models.ViewModels
{
    public class PermissionFormView
    {
        public IEnumerable<Pessoa> Pessoa { get; set; }
        public IEnumerable<Profissao> Profissao { get; set; }
        public bool Permission { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crud_mvc.Models.Enums;

namespace crud_mvc.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Gênero")]
        public Generos Genero { get; set; }

        [Column("data_nascimento")]
        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }
        public Estados Estado { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public float Peso { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public float Altura { get; set; }
        public int Idade { get; set; }
        [Column("id_profissao")]
        public int ProfissaoId { get; set; }
        [Display(Name = "Profissão")]
        public Profissao Profissao { get; set; }

        public Pessoa() { }

        public Pessoa(int id, string cpf, string nome, Generos genero, DateTime dataNascimento, Estados estadoNascimento, float peso, float altura, Profissao profissao)
        {
            Id = id;
            Cpf = cpf;
            Nome = nome;
            Genero = genero;
            DataNascimento = dataNascimento;
            Estado = estadoNascimento;
            Peso = peso;
            Altura = altura;
            Profissao = profissao;
        }


        public void GeraIdade(DateTime obj)
        {
            Idade = DateTime.Now.Year - obj.Year;
        }
    }
}

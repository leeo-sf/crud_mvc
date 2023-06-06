using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crud_mvc.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Gênero")]
        [Column("genero")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        [Column("data_nascimento")]
        [Display(Name = "Data Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Column("estado")]
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

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

        public Pessoa(int id, string cpf, string nome, Genero genero, DateTime dataNascimento, Estado estadoNascimento, float peso, float altura, Profissao profissao)
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

        public string FormataCpf()
        {
            var newCpf = Cpf.Substring(0, 3) + "." + Cpf.Substring(3,3) + "." + Cpf.Substring(6,3) + "-" + Cpf.Substring(9,2);
            return newCpf;
        }

        public bool ValidaCPF()
        {
            if (Cpf.Length != 11)
            {
                return true;
            }
            for (int count = 0; count < this.Cpf.Length; count++)
            {
                if (this.Cpf[count].ToString() == "-" || this.Cpf[count] == '/')
                {
                    throw new Exception("Digite somente os números do CPF.");
                }
            }

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string newCpf = this.Cpf.Substring(0, 9);
            int total = 0;
            int resto;
            string digito;

            for (int count = 0; count < 9; count++)
            {
                total += int.Parse(newCpf[count].ToString()) * mult1[count];
            }
            resto = total % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            newCpf += digito;
            total = 0;

            for (int count = 0; count < 10; count++)
            {
                total += int.Parse(newCpf[count].ToString()) * mult2[count];
            }
            resto = total % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            newCpf += digito;

            if (!(this.Cpf == newCpf && this.ConfereSequencia(newCpf)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ConfereSequencia(string cpf)
        {
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
                default:
                    return true;
            }
        }
    }
}

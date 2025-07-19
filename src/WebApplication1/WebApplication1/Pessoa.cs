using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public Endereco Endereco { get; set; }
    }

    public class Endereco
    {
        [Required]
        public int? Numero { get; set; }
        public string Rua { get; set; } 
        public string Cidade { get; set; } 
        public string Estado { get; set; }
    }
}

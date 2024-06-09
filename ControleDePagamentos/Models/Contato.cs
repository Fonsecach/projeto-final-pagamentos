using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace ControleDePagamentos.Models
{
    public class Contato
    {
        [Key]
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? WhatsApp { get; set; }
        public string? Telefone { get; set; }
        // Relacionamento com Pessoa (muitos para 1)
        [ForeignKey("PessoaID")]
        public int PessoaID { get; set; }
    }
}
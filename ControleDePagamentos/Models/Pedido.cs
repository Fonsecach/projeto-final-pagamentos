using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
public class Pedido
    {
        public int ID { get; set; }
        public string? Descricao { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
        public decimal Valor { get; set; }
        public DateTime DataDoPedido { get; set; } = DateTime.Now;
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        [ForeignKey("PagamentoID")]
        public int PagamentoID { get; set; }
        public Pagamento? Pagamento { get; set; }
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        public Pessoa? Credor { get; set; }
    }
}
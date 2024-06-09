using System.ComponentModel.DataAnnotations.Schema;
using ControleDePagamentos.Models;

namespace Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDePagamento { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }

        // Chave estrangeira para Pedido
        [ForeignKey("PedidoID")]
        public int PedidoID { get; set; }
        public Pedido? Pedido { get; set; }

        // Chave estrangeira para Pessoa (Devedor)
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }
        public Pessoa? Devedor { get; set; }

        // Chave estrangeira para Pessoa (Credor)
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        public Pessoa? Credor { get; set; }

        // Relacionamento com Parcelas
        public ICollection<Parcela>? Parcelas { get; set; }
    }
}
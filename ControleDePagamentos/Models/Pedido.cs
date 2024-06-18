using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDePagamentos.Models
{
    public class Pedido
    {
        public int ID { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataDoPedido { get; set; }
        public DateTime DataDoVencimento { get; set; }
        public string Status { get; set; } = "Aguardando Pagamento";

        // Chave estrangeira para Pessoa (Devedor)
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }

        // Chave estrangeira para Pessoa (Credor)
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
    }
}
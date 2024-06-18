using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleDePagamentos.Models;

namespace Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
        public decimal Valor { get; set; }
        public DateTime? DataDePagamento { get; set; }
        public TipoPagamento Tipo { get; set; } = 0;
        public FormaPagamento? Forma { get; set; } = 0;
        public int QuantidadeParcelas { get; set; } = 1;
        [Required]
        public DateTime DataDoVencimento { get; set; }
        public string Status { get; set; } = "Aguardando Pagamento";
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        // Chave estrangeira para Pedido
        public int PedidoID { get; set; }
        [ForeignKey("PedidoID")]
        public Pedido? Pedido { get; set; }
        
        // Chave estrangeira para Devedor (Pessoa)
        public int DevedorID { get; set; }
        [ForeignKey("DevedorID")]
        public Pessoa? Devedor { get; set; }
        // Chave estrangeira para Credor (Pessoa)
        public int CredorID { get; set; }
        [ForeignKey("CredorID")]
        public Pessoa? Credor { get; set; }

        public enum TipoPagamento
        {
            AVista,
            Parcelado
        }

        public enum FormaPagamento
        {
            Pix,
            Dinheiro,
            Boleto,
            Credito,
            Ted_Doc,
            Debito
        }

        public ICollection<Parcela>? Parcelas { get; set; }
    }
}

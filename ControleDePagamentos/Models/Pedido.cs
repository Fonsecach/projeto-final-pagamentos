using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
public class Pedido
    {
        public int ID { get; set; }
        public string? Descricao { get; set; }
        public TipoPagamento Tipo { get; set; }
        public FormaPagamento? Forma { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime DataDoPedido { get; set; }
        public DateTime DataDoVencimento { get; set; }
        public string Status { get; set; } = "Aguardando Pagamento";
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }

        // Chave estrangeira para Pessoa (Devedor)
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }
        public Pessoa? Devedor { get; set; }

        // Chave estrangeira para Pessoa (Credor)
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        public Pessoa? Credor { get; set; }
    }

    public enum TipoPagamento
    {
        AVista,
        Parcelado
    }

    public enum FormaPagamento
    {
        Dinheiro,
        Pix,
        Boleto,
        Credito,
        Ted_Doc,
        Debito
    }
}
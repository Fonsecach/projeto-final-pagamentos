using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace ControleDePagamentos.Models
{
    public class Parcela
    {
        [Key]
        public int ID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public DateTime? DataDePagamento { get; set; }

        // Chave estrangeira para Pagamento
        [ForeignKey("PagamentoID")]
        public int PagamentoID { get; set; }
        public Pagamento? Pagamento { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models;

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
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        [JsonIgnore]
        public Pessoa? Devedor { get; set; }
        [JsonIgnore]
        public Pessoa? Credor { get; set; }
    }
}
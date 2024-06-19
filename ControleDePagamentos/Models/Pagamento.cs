using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models;

namespace ControleDePagamentos.Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDePagamento { get; set; }
        [ForeignKey("PedidoID")]
        public int PedidoID { get; set; }
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        [JsonIgnore]
        public Pedido? Pedido { get; set; }
        [JsonIgnore]
        public Pessoa? Devedor { get; set; }
        [JsonIgnore]
        public Pessoa? Credor { get; set; }
    }
}
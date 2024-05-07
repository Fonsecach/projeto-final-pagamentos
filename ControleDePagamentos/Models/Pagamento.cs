using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
public class Pagamento
    {
        public int ID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDePagamento { get; set; }

        // Chave estrangeira para Pedido
        [ForeignKey("PedidoID")]
        public int PedidoID { get; set; }
        
        // Chave estrangeira para Pessoa (Devedor)
        [ForeignKey("DevedorID")]
        public int DevedorID { get; set; }

        // Chave estrangeira para Pessoa (Credor)
        [ForeignKey("CredorID")]
        public int CredorID { get; set; }
        
    }
}
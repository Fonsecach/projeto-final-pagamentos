using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
public class Pedido
{
    public int ID { get; set; }
    public int DevedorID { get; set; }
    public int CredorID { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataDoPedido { get; set; }

    // Relacionamento com Pessoa (Devedor e Credor)
    public Pessoa Devedor { get; set; }
    public Pessoa Credor { get; set; }
    }
}
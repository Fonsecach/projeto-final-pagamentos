using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        public int DevedorID { get; set; }
        public int CredorID { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDePagamento { get; set; }

        // Relacionamento com Pessoa (Devedor e Credor)
        public Pessoa Devedor { get; set; }
        public Pessoa Credor { get; set; }  
    }
}
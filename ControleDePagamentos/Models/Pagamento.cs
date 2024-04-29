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

        // Chave estrangeira para Pessoa (Devedor)
        public int DevedorID { get; set; }
        [ForeignKey("DevedorID")] // Anotação para indicar a chave estrangeira
        public Pessoa? Devedor { get; set; }

        // Chave estrangeira para Pessoa (Credor)
        public int CredorID { get; set; }
        [ForeignKey("CredorID")] // Anotação para indicar a chave estrangeira
        public Pessoa? Credor { get; set; }
    }
}
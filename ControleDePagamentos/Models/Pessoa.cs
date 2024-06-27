using System.ComponentModel.DataAnnotations;
using ControleDePagamentos.Models;

namespace Models
{
    public class Pessoa
    {
        [Key]
        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? NomeFantasia { get; set; }
        public string? NumDocumento { get; set; }
        public string? Tipo { get; set; }
        public List<Endereco>? Enderecos { get; set; }
        public List<Contato>? Contatos { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        public string? Observacoes { get; set; }
    }
}
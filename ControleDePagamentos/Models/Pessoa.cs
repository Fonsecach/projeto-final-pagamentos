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
        public TipoPessoa Tipo { get; set; }
        // Relacionamento com Endereço (1 para muitos)
        public List<Endereco>? Enderecos { get; set; }
        // Relacionamento com Contato (1 para muitos)
        public List<Contato>? Contatos { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        public string? Observacoes { get; set; }

    }

    public enum TipoPessoa
    {
        Fisica,
        Juridica
    }
}
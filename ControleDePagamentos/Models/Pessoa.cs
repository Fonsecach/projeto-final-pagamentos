using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Pessoa
    {
        [Key]
        public int ID { get; set; }
        public string? Nome { get; set; }
        public string? Documento { get; set; }
        public TipoPessoa Tipo { get; set; }
        // Relacionamento com Endereço (1 para muitos)
        public List<Endereco>? Enderecos { get; set; }
        // Relacionamento com Contato (1 para muitos)
        public List<Contato>? Contatos { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
    }
    public class Endereco
    {
        [Key]
        public int ID { get; set; }
        public string? EnderecoCompleto { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }

        // Chave estrangeira para Pessoa (muitos para 1)
        public int PessoaID { get; set; }
        [ForeignKey("PessoaID")] // Anotação para indicar a chave estrangeira
        public Pessoa? Pessoa { get; set; }
    }

    public class Contato
    {
        [Key]
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? WhatsApp { get; set; }
        public string? Telefone { get; set; }
        public string? Observacoes { get; set; }

        // Relacionamento com Pessoa (muitos para 1)
        public int PessoaID { get; set; }
        [ForeignKey("PessoaID")] // Anotação para indicar a chave estrangeira
        public Pessoa? Pessoa { get; set; }
    }

    public enum TipoPessoa
    {
        Fisica,
        Juridica
    }
}
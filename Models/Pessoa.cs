using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoPessoa Tipo { get; set; }
        // Relacionamento com Endereço (1 para muitos)
        public List<Endereco> Enderecos { get; set; }
        // Relacionamento com Contato (1 para muitos)
        public List<Contato> Contatos { get; set; }
        public datetime CriadoEm { get; set; } = DateTime.Now;
    }
    public class Endereco
    {
        public int ID { get; set; }
        public int PessoaID { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        // Relacionamento com Pessoa (muitos para 1)
        public Pessoa Pessoa { get; set; }
    }

    public class Contato
    {
        public int ID { get; set; }
        public int PessoaID { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public string Telefone { get; set; }
        public string Observacoes { get; set; }

        // Relacionamento com Pessoa (muitos para 1)
        public Pessoa Pessoa { get; set; }
    }

    public enum TipoPessoa
    {
        Fisica,
        Juridica
    }
}
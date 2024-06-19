using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;


namespace ControleDePagamentos.Models
{
    public class Endereco
    {
    [Key]
    public int ID { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }
    // Chave estrangeira para Pessoa (muitos para 1)
    [ForeignKey("PessoaID")] 
    public int PessoaID { get; set; }
    }
        
}

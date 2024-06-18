using Microsoft.EntityFrameworkCore;
using Models;
using ControleDePagamentos.Models;

public class AppDataContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Parcela> Parcelas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.sqlite");
    }

}

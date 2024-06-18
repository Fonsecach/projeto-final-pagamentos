using Microsoft.EntityFrameworkCore;
using Models;

namespace ControleDePagamentos.Models
{
    public class AppDataContext : DbContext
    {
        //EF Code First

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.sqlite");
        }

    }
}

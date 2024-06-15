using Microsoft.EntityFrameworkCore;
using ControleDePagamentos.Models;

namespace Models
{
    public class AppDataContext : DbContext
    {
        //EF Code First

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.sqlite"); // Caminho para o arquivo SQLite - String de conexão
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamento>()
            .HasOne(p => p.Pedido)
            .WithMany()
            .HasForeignKey(p => p.PedidoID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pagamento>()
            .HasOne(p => p.Devedor)
            .WithMany()
            .HasForeignKey(p => p.DevedorID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pagamento>()
            .HasOne(p => p.Credor)
            .WithMany()
            .HasForeignKey(p => p.CredorID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pagamento>()
            .HasMany(p => p.Parcelas)
            .WithOne(p => p.Pagamento)
            .HasForeignKey(p => p.PagamentoID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Devedor)
            .WithMany()
            .HasForeignKey(p => p.DevedorID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Credor)
            .WithMany()
            .HasForeignKey(p => p.CredorID)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices para melhoria de performance
        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.DevedorID)
            .HasDatabaseName("IX_Pedido_DevedorID");

        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.CredorID)
            .HasDatabaseName("IX_Pedido_CredorID");

        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.Descricao)
            .HasDatabaseName("IX_Pedido_Descricao");

        modelBuilder.Entity<Pedido>()
            .HasIndex(p => p.ID)
            .HasDatabaseName("IX_Pedido_ID");
    }
    }
}
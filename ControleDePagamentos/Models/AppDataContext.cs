using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
    }
}
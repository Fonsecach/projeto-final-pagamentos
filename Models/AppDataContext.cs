using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ControleDepagamentos.Models
{
    public class AppDataContext : DbContext
    {
        //EF Code First

        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Pagamentos> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.sqlite"); // Caminho para o arquivo SQLite - String de conexão
        }
    }
}
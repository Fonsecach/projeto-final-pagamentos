using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Pessoas> Pessoas { get; set; }
    public DbSet<Pedidos> Pedidos { get; set; }
    public DbSet<Pagamentos> Pagamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ControleDePagamentos.db");
    }
}
}
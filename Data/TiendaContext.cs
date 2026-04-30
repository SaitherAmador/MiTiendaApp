using Microsoft.EntityFrameworkCore;
using MiTiendaApp.Models;

public class TiendaContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=tienda.db");
}

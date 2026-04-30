using Microsoft.EntityFrameworkCore;
using MiTiendaApp.Models;

namespace MiTiendaApp.Services;

public class ProductService
{
    private readonly TiendaContext _context;

    public ProductService(TiendaContext context)
    {
        _context = context;
    }

    public List<Producto> GetAll()
    {
        return _context.Productos
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nombre)
            .ToList();
    }

    public Producto? GetById(int id)
    {
        return _context.Productos.Find(id);
    }

    public List<Producto> SearchByName(string texto)
    {
        return _context.Productos
            .Include(p => p.Categoria)
            .Where(p => p.Nombre!.Contains(texto))
            .ToList();
    }

    public Producto Add(string nombre, decimal precio, int stock, int categoriaId)
    {
        var nuevo = new Producto
        {
            Nombre = nombre,
            Precio = precio,
            Stock = stock,
            CategoriaId = categoriaId
        };
        _context.Productos.Add(nuevo);
        _context.SaveChanges();
        return nuevo;
    }

    public void Update(int id, decimal? nuevoPrecio, int? nuevoStock)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null) return;

        if (nuevoPrecio.HasValue) producto.Precio = nuevoPrecio.Value;
        if (nuevoStock.HasValue) producto.Stock = nuevoStock.Value;

        _context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        _context.SaveChanges();
        return true;
    }
}

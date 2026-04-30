using Microsoft.EntityFrameworkCore;
using MiTiendaApp.Models;

namespace MiTiendaApp.Services;

public class CategoryService
{
    private readonly TiendaContext _context;

    public CategoryService(TiendaContext context)
    {
        _context = context;
    }

    public List<Categoria> GetAll()
    {
        return _context.Categorias
            .Include(c => c.Productos)
            .OrderBy(c => c.Nombre)
            .ToList();
    }

    public Categoria? GetById(int id)
    {
        return _context.Categorias.Find(id);
    }

    public bool Exists(int id)
    {
        return _context.Categorias.Any(c => c.Id == id);
    }

    public bool HasAny()
    {
        return _context.Categorias.Any();
    }

    public Categoria Add(string nombre)
    {
        var cat = new Categoria { Nombre = nombre };
        _context.Categorias.Add(cat);
        _context.SaveChanges();
        return cat;
    }
}

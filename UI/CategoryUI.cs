using MiTiendaApp.Services;

namespace MiTiendaApp.UI;

public class CategoryUI
{
    private readonly CategoryService _categoryService;

    public CategoryUI(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public void Ver()
    {
        ConsoleHelper.Color("\n  -- CATEGORÍAS --\n", ConsoleColor.Cyan);

        var cats = _categoryService.GetAll();

        if (!cats.Any())
        {
            ConsoleHelper.Color("  No hay categorías.\n", ConsoleColor.Yellow);
            return;
        }

        foreach (var cat in cats)
            Console.WriteLine($"  [{cat.Id}] {cat.Nombre} — {cat.Productos.Count} producto(s)");
    }

    public void Agregar()
    {
        ConsoleHelper.Color("\n  -- AGREGAR CATEGORÍA --\n", ConsoleColor.Green);
        Console.Write("  Nombre de la categoría: ");
        string nombre = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            ConsoleHelper.Color("  Nombre inválido.\n", ConsoleColor.Red);
            return;
        }

        var cat = _categoryService.Add(nombre);
        ConsoleHelper.Color($"  ✓ Categoría '{cat.Nombre}' creada con Id = {cat.Id}\n", ConsoleColor.Green);
    }
}

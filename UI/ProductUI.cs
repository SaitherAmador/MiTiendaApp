using MiTiendaApp.Models;
using MiTiendaApp.Services;

namespace MiTiendaApp.UI;

public class ProductUI
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public ProductUI(ProductService productService, CategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public void Listar()
    {
        var productos = _productService.GetAll();

        if (!productos.Any())
        {
            ConsoleHelper.Color("  No hay productos registrados.\n", ConsoleColor.Yellow);
            return;
        }

        ConsoleHelper.Color($"\n  {"ID",-4} {"Nombre",-20} {"Precio",10} {"Stock",6} {"Categoría",-15}\n", ConsoleColor.White);
        ConsoleHelper.Color($"  {new string('-', 60)}\n", ConsoleColor.DarkGray);

        foreach (var p in productos)
        {
            Console.WriteLine($"  {p.Id,-4} {p.Nombre,-20} ${p.Precio,9:F2} {p.Stock,6}  {p.Categoria?.Nombre ?? "—",-15}");
        }

        ConsoleHelper.Color($"\n  Total: {productos.Count} producto(s)\n", ConsoleColor.Cyan);
    }

    public void Agregar()
    {
        ConsoleHelper.Color("\n  -- AGREGAR PRODUCTO --\n", ConsoleColor.Green);

        var categorias = _categoryService.GetAll();
        if (!categorias.Any())
        {
            ConsoleHelper.Color("  Primero crea al menos una categoría (opción 7).\n", ConsoleColor.Yellow);
            return;
        }

        Console.WriteLine("  Categorías disponibles:");
        foreach (var cat in categorias)
            Console.WriteLine($"    [{cat.Id}] {cat.Nombre}");

        Console.Write("\n  Nombre del producto : ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("  Precio (ej: 299.99) : ");
        decimal precio = decimal.TryParse(Console.ReadLine(), out var precioOut) ? precioOut : 0;

        Console.Write("  Stock inicial       : ");
        int stock = int.TryParse(Console.ReadLine(), out var stockOut) ? stockOut : 0;

        Console.Write("  ID de categoría     : ");
        int catId = int.TryParse(Console.ReadLine(), out var catIdOut) ? catIdOut : 0;

        if (string.IsNullOrWhiteSpace(nombre) || precio <= 0 || !_categoryService.Exists(catId))
        {
            ConsoleHelper.Color("  Datos inválidos o categoría no existe. Operación cancelada.\n", ConsoleColor.Red);
            return;
        }

        var nuevo = _productService.Add(nombre, precio, stock, catId);
        ConsoleHelper.Color($"\n  ✓ Producto '{nuevo.Nombre}' creado con Id = {nuevo.Id}\n", ConsoleColor.Green);
    }

    public void Buscar()
    {
        ConsoleHelper.Color("\n  -- BUSCAR PRODUCTO --\n", ConsoleColor.Cyan);
        Console.Write("  Texto a buscar en el nombre: ");
        string texto = Console.ReadLine() ?? "";

        var resultados = _productService.SearchByName(texto);

        if (!resultados.Any())
        {
            ConsoleHelper.Color($"  No se encontró ningún producto con '{texto}'.\n", ConsoleColor.Yellow);
            return;
        }

        ConsoleHelper.Color($"\n  Resultados para '{texto}':\n", ConsoleColor.White);
        foreach (var p in resultados)
            Console.WriteLine($"  [{p.Id}] {p.Nombre} — ${p.Precio:F2} — Stock: {p.Stock} — Cat: {p.Categoria?.Nombre}");
    }

    public void Editar()
    {
        ConsoleHelper.Color("\n  -- EDITAR PRODUCTO --\n", ConsoleColor.Yellow);
        Listar();
        Console.Write("\n  ID del producto a editar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.Color("  ID inválido.\n", ConsoleColor.Red);
            return;
        }

        var producto = _productService.GetById(id);
        if (producto == null)
        {
            ConsoleHelper.Color($"  No existe un producto con Id {id}.\n", ConsoleColor.Red);
            return;
        }

        Console.WriteLine($"\n  Editando: {producto.Nombre}");
        Console.Write($"  Nuevo precio (actual ${producto.Precio:F2}, ENTER para mantener): ");
        string inputPrecio = Console.ReadLine() ?? "";

        Console.Write($"  Nuevo stock  (actual {producto.Stock}, ENTER para mantener): ");
        string inputStock = Console.ReadLine() ?? "";

        decimal? nuevoPrecio = decimal.TryParse(inputPrecio, out var p) ? p : null;
        int? nuevoStock = int.TryParse(inputStock, out var s) ? s : null;

        _productService.Update(id, nuevoPrecio, nuevoStock);

        producto = _productService.GetById(id)!;
        ConsoleHelper.Color($"  ✓ Producto actualizado: {producto.Nombre} | ${producto.Precio:F2} | Stock: {producto.Stock}\n", ConsoleColor.Green);
    }

    public void Eliminar()
    {
        ConsoleHelper.Color("\n  -- ELIMINAR PRODUCTO --\n", ConsoleColor.Red);
        Listar();
        Console.Write("\n  ID del producto a eliminar: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            ConsoleHelper.Color("  ID inválido.\n", ConsoleColor.Red);
            return;
        }

        var producto = _productService.GetById(id);
        if (producto == null)
        {
            ConsoleHelper.Color($"  No existe un producto con Id {id}.\n", ConsoleColor.Red);
            return;
        }

        Console.Write($"  ¿Eliminar '{producto.Nombre}'? (s/N): ");
        if (Console.ReadLine()?.ToLower() != "s")
        {
            ConsoleHelper.Color("  Cancelado.\n", ConsoleColor.Yellow);
            return;
        }

        _productService.Delete(id);
        ConsoleHelper.Color($"  ✓ Producto '{producto.Nombre}' eliminado.\n", ConsoleColor.Green);
    }
}

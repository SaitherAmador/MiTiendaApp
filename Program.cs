using Microsoft.EntityFrameworkCore;
using MiTiendaApp.Services;
using MiTiendaApp.UI;

class Program
{
    static void Main()
    {
        using var context = new TiendaContext();
        context.Database.EnsureCreated();

        var categoryService = new CategoryService(context);
        var productService = new ProductService(context);
        var dataSeeder = new DataSeeder(categoryService, productService);
        dataSeeder.Seed(context);

        var productUI = new ProductUI(productService, categoryService);
        var categoryUI = new CategoryUI(categoryService);

        bool salir = false;
        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine()?.Trim() ?? "";

            Console.WriteLine();
            switch (opcion)
            {
                case "1": productUI.Listar(); break;
                case "2": productUI.Agregar(); break;
                case "3": productUI.Buscar(); break;
                case "4": productUI.Editar(); break;
                case "5": productUI.Eliminar(); break;
                case "6": categoryUI.Ver(); break;
                case "7": categoryUI.Agregar(); break;
                case "0": salir = true; break;
                default:
                    ConsoleHelper.Color("  ⚠  Opción no válida.\n", ConsoleColor.Yellow);
                    break;
            }

            if (!salir)
            {
                ConsoleHelper.WaitForEnter();
            }
        }

        ConsoleHelper.Color("\n  ¡Hasta luego! Los datos quedaron guardados en tienda.db\n\n", ConsoleColor.Cyan);
    }

    static void MostrarMenu()
    {
        Console.Clear();
        ConsoleHelper.Color("╔══════════════════════════════════════════╗\n", ConsoleColor.Cyan);
        ConsoleHelper.Color("║   🛒  TIENDA — Entity Framework Core     ║\n", ConsoleColor.Cyan);
        ConsoleHelper.Color("╠══════════════════════════════════════════╣\n", ConsoleColor.Cyan);
        Console.WriteLine("║  PRODUCTOS                               ║");
        Console.WriteLine("║   1 → Listar todos los productos         ║");
        Console.WriteLine("║   2 → Agregar nuevo producto             ║");
        Console.WriteLine("║   3 → Buscar producto por nombre         ║");
        Console.WriteLine("║   4 → Editar precio / stock              ║");
        Console.WriteLine("║   5 → Eliminar producto                  ║");
        Console.WriteLine("║  CATEGORÍAS                              ║");
        Console.WriteLine("║   6 → Ver categorías                     ║");
        Console.WriteLine("║   7 → Agregar categoría                  ║");
        ConsoleHelper.Color("║   0 → Salir                              ║\n", ConsoleColor.DarkGray);
        ConsoleHelper.Color("╚══════════════════════════════════════════╝\n", ConsoleColor.Cyan);
        Console.Write("  Elige una opción: ");
    }
}

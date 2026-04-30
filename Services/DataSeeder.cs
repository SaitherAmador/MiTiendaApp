using MiTiendaApp.Models;
using MiTiendaApp.Services;

namespace MiTiendaApp.Services;

public class DataSeeder
{
    private readonly CategoryService _categoryService;
    private readonly ProductService _productService;

    public DataSeeder(CategoryService categoryService, ProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    public void Seed(TiendaContext context)
    {
        if (_categoryService.HasAny()) return;

        var electronica = _categoryService.Add("Electrónica");
        var ropa = _categoryService.Add("Ropa");

        _productService.Add("Laptop Pro", 1200.00m, 10, electronica.Id);
        _productService.Add("Teléfono X", 799.99m, 25, electronica.Id);
        _productService.Add("Audífonos BT", 89.99m, 40, electronica.Id);
        _productService.Add("Camiseta Polo", 29.99m, 60, ropa.Id);
        _productService.Add("Jeans Slim", 59.99m, 35, ropa.Id);
    }
}

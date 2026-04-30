# MiTiendaApp - Entity Framework Core

Sistema de tienda interactivo para consola construido con **Entity Framework Core** y **SQLite**. Permite gestionar productos y categorías mediante operaciones CRUD completas.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download) o superior
- Terminal o línea de comandos

## Estructura del Proyecto

```
MiTiendaApp/
├── Models/
│   ├── Producto.cs          # Entidad Producto
│   └── Categoria.cs         # Entidad Categoría
├── Data/
│   └── TiendaContext.cs     # DbContext (configuración de BD)
├── Services/
│   ├── ProductService.cs    # Lógica CRUD de productos
│   ├── CategoryService.cs   # Lógica CRUD de categorías
│   └── DataSeeder.cs        # Datos iniciales de ejemplo
├── UI/
│   ├── ConsoleHelper.cs     # Utilidades de consola (colores)
│   ├── ProductUI.cs         # Interfaz de usuario para productos
│   └── CategoryUI.cs        # Interfaz de usuario para categorías
└── Program.cs               # Menú principal y punto de entrada
```

## Cómo Ejecutar el Proyecto

### Paso 1: Clonar el repositorio

```bash
git clone https://github.com/SaitherAmador/MiTiendaApp.git
cd MiTiendaApp
```

### Paso 2: Instalar dependencias

```bash
dotnet restore
```

Esto descarga los paquetes NuGet necesarios:
- `Microsoft.EntityFrameworkCore.Sqlite` — Proveedor de SQLite para EF Core
- `Microsoft.EntityFrameworkCore.Design` — Herramientas de diseño para EF Core

### Paso 3: Ejecutar la aplicación

```bash
dotnet run
```

Al ejecutarse por primera vez, se crea automáticamente el archivo `tienda.db` con datos de ejemplo (5 productos en 2 categorías).

### Paso 4: Usar el menú

El menú interactivo permite:

| Opción | Acción |
|--------|--------|
| 1 | Listar todos los productos |
| 2 | Agregar nuevo producto |
| 3 | Buscar producto por nombre |
| 4 | Editar precio o stock de un producto |
| 5 | Eliminar un producto |
| 6 | Ver categorías con conteo de productos |
| 7 | Agregar nueva categoría |
| 0 | Salir |

## Cómo Funciona

### Base de datos

La aplicación usa **SQLite** y la base de datos se crea automáticamente en `tienda.db`. El contexto está configurado en `Data/TiendaContext.cs`:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=tienda.db");
```

### Arquitectura en capas

- **Models** → Clases que representan las tablas de la base de datos
- **Data** → `TiendaContext` que conecta C# con SQLite mediante EF Core
- **Services** → Contienen toda la lógica de negocio (CRUD)
- **UI** → Manejan la interacción con el usuario por consola
- **Program** → Solo el menú principal que coordina todo

### Relación entre entidades

```
Categoria 1 ──── ∞ Producto
```
Cada producto pertenece a una categoría. Cada categoría puede tener muchos productos.

## Comandos Útiles

| Comando | Descripción |
|---------|-------------|
| `dotnet build` | Compilar el proyecto |
| `dotnet run` | Ejecutar la aplicación |
| `dotnet clean` | Limpiar archivos compilados |
| `dotnet restore` | Restaurar paquetes NuGet |

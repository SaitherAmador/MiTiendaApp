namespace MiTiendaApp.UI;

public static class ConsoleHelper
{
    public static void Color(string texto, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(texto);
        Console.ResetColor();
    }

    public static void WaitForEnter()
    {
        Console.Write("\n  Presiona ENTER para continuar...");
        Console.ReadLine();
    }
}

// File: helpers/UIHelpers.cs (Puedes renombrar el archivo si gustas)
namespace helpers;

public static class UIHelpers
{
    public static void PrintTitle(string title)
    {
        WriteLine($"\n«« {title} »»");
    }

    public static void PrintDescription(string desc)
    {
        WriteLine($"|| {desc} ||");
    }

    // ¡Nuevos Helpers para estandarizar los mensajes del sistema!
    public static void PrintSuccess(string message)
    {
        WriteLine($"\n[Success] {message}");
    }

    public static void PrintError(string message)
    {
        WriteLine($"[Error] {message}");
    }

    public static void PrintInfo(string message)
    {
        WriteLine($"[Info] {message}");
    }
}
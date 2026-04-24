namespace modules_activities.Utils;

public static class Logger
{
    // Nombre del archivo que se va a crear automáticamente
    private static readonly string rutaArchivo = "error_log.txt";

    public static void LogError(string contexto, Exception ex)
    {
        // 1. Armamos el mensaje con fecha, hora, dónde ocurrió y el error real
        string fechaHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string mensajeLog = $"[{fechaHora}] ERROR en {contexto} | Detalle técnico: {ex.Message}{Environment.NewLine}";

        // 2. Guardamos en el archivo de texto (AppendAllText crea el archivo si no existe, 
        // y si ya existe, añade la línea al final sin borrar lo anterior).
        File.AppendAllText(rutaArchivo, mensajeLog);

        // 3.
        /* * ¿CÓMO AYUDA ESTO EN UN ENTORNO REAL?
         * En producción, los usuarios no deben ver errores técnicos complejos porque es inseguro
         * y confuso. Este archivo de Log permite al equipo de soporte técnico rastrear fallos silenciosos,
         * ver la hora exacta del problema y leer el "Stack Trace" real para lanzar parches rápidamente,
         * mientras que al usuario solo se le muestra un mensaje amigable.
         */
    }
}
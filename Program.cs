
using services;
using helpers;
using modules_activities.Exceptions;
using modules_activities.Utils;

internal partial class Program
{
    static void Main(string[] args)
    {
        // prueba del trycatch
        Console.WriteLine("\n--- SISTEMA DE BÚSQUEDA DE MASCOTAS ---");

    // 1. INTENTAMOS EJECUTAR EL CÓDIGO
        try 
        {
            Console.WriteLine("Buscando a la mascota 'Pelusa' en el sistema...");
    
            bool mascotaExiste = false; // Simulamos que buscamos en BD y no está
    
            if (!mascotaExiste)
            {
                // Forzamos (lanzamos) nuestro propio error personalizado
                throw new PetNotFinded("La mascota 'Pelusa' no tiene un historial médico registrado.");
            }

            Console.WriteLine("Mascota encontrada. Abriendo historial..."); // Esto no se ejecutará
        }
    // 2. ATRAPAMOS ERRORES ESPECÍFICOS DE LA CLÍNICA
        catch (PetNotFinded ex)
        {
            // msg to user
            Console.WriteLine($"[AVISO]: {ex.Message}");            
            Console.WriteLine("Sugerencia: Verifique el nombre o registre una nueva mascota.");
            Logger.LogError("Búsqueda de Mascota (PetNotFindedException)", ex);
        }
// 3. ATRAPAMOS CUALQUIER OTRO ERROR INESPERADO (Ej. Se cayó el internet)
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]: Ocurrió un problema, inténtelo más tarde. Soporte técnico ha sido notificado.");
            Logger.LogError("Fallo General Inesperado", ex);
        }
// 4. (Siempre se ejecuta)
        finally
        {
            Console.WriteLine("[SISTEMA]: Cerrando conexión con la base de datos de búsqueda.");
        }
        // Instanciamos ambos servicios
        PatientService dataService = new PatientService();
        ClinicalReports reportsService = new ClinicalReports();
        
        bool exitProgram = false;

        do
        {
            // Clear(); // borrar clear para ver el funcionamiento de la task 5(try-catch-finally) y 6
            UIHelpers.PrintTitle("Clinic Health+ Management System");
            WriteLine("1. Register new patient and pet");
            WriteLine("2. List all records");
            WriteLine("3. Search patient by name");
            WriteLine("4. Attend Patient (Veterinary Services)"); // <-- Nueva implementación
            WriteLine("5. Advanced Analytics (LINQ Reports)"); // Nueva opción
            WriteLine("6. Exit");
            Write("\nSelect an option: ");

            string userChoice = ReadLine()?.Trim() ?? "";

            exitProgram = userChoice switch
            {
                "1" => dataService.RegisterPatient(),
                "2" => dataService.ListPatients(),
                "3" => dataService.SearchPatient(),
                "4" => dataService.AttendPatient(),
                "5" => ShowAnalyticsMenu(dataService, reportsService), // Llamada al submenú,
                "6" => true,
                _   => dataService.ShowError()
            };

        } while (!exitProgram);

        UIHelpers.PrintInfo("Closing the application. Goodbye!");
    }

    // --- SUBMENÚ DE REPORTES ---
    // Lo hacemos estático para poder llamarlo desde el Main
    static bool ShowAnalyticsMenu(PatientService data, ClinicalReports reports)
    {
        bool backToMain = false;
        
        // Aquí usamos la "puerta de lectura" que creamos en el Paso 1
        var currentData = data.GetPatientsDatabase(); 

        do
        {
            Clear();
            UIHelpers.PrintTitle("Advanced Analytics & LINQ");
            WriteLine("1. Dictionary Fast Lookup (By ID)");
            WriteLine("2. Dog Owners Report (Ordered by Age)");
            WriteLine("3. Demographics: Patients grouped by Pet Species");
            WriteLine("4. Solve Practical Problems (Min/Max, Any, Select)");
            WriteLine("5. Back to Main Menu");
            Write("\nSelect an option: ");

            string choice = ReadLine()?.Trim() ?? "";

            // Usamos un switch tradicional aquí para variar y manejar el 'break' del submenú
            switch (choice)
            {
                case "1": 
                    reports.DemonstrateDictionaryUsage(currentData); 
                    break;
                case "2": 
                    reports.ShowDogOwnersOrderedByAge(currentData); 
                    break;
                case "3": 
                    reports.ShowPatientsGroupedByPetSpecies(currentData); 
                    break;
                case "4": 
                    reports.SolvePracticalProblems(currentData); 
                    break;
                case "5": 
                    backToMain = true; 
                    break;
                default: 
                    UIHelpers.PrintError("Invalid option."); 
                    ReadLine(); 
                    break;
            }
        } while (!backToMain);

        // Retornamos false para que el Main Menu no se cierre al volver
        return false; 
    }
}
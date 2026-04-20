
using services;
using helpers;

internal partial class Program
{
    static void Main(string[] args)
    {
        // Instanciamos ambos servicios
        PatientService dataService = new PatientService();
        ClinicalReports reportsService = new ClinicalReports();
        
        bool exitProgram = false;

        do
        {
            Clear();
            UIHelpers.PrintTitle("Clinic Health+ Management System");
            WriteLine("1. Register new patient and pet");
            WriteLine("2. List all records");
            WriteLine("3. Search patient by name");
            WriteLine("4. Advanced Analytics (LINQ Reports)"); // Nueva opción
            WriteLine("5. Exit");
            Write("\nSelect an option: ");

            string userChoice = ReadLine()?.Trim() ?? "";

            exitProgram = userChoice switch
            {
                "1" => dataService.RegisterPatient(),
                "2" => dataService.ListPatients(),
                "3" => dataService.SearchPatient(),
                "4" => ShowAnalyticsMenu(dataService, reportsService), // Llamada al submenú
                "5" => true,
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
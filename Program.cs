using services;
using helpers;

partial class Program
{
    // svm (static void Main)
    static void Main(string[] args)
    {
        // Instanciamos el servicio para poder usar sus metodos
        PatientService service = new PatientService();
        
        bool exitProgram = false;

        do
        {
            Clear();
            UIHelpers.PrintTitle("Clinic Health+ Management System");
            WriteLine("1. Register new patient and pet");
            WriteLine("2. List all records");
            WriteLine("3. Search patient by name");
            WriteLine("4. Exit");
            Write("\nSelect an option: ");

            string userChoice = ReadLine()?.Trim() ?? "";

            // Switch expression que maneja la navegación y retorna un booleano
            exitProgram = userChoice switch
            {
                "1" => service.RegisterPatient(),
                "2" => service.ListPatients(),
                "3" => service.SearchPatient(),
                "4" => true, // Rompe el ciclo do-while
                _   => service.ShowError() // Caso por defecto para entradas inválidas
            };

        } while (!exitProgram);

        UIHelpers.PrintInfo("Closing the application. Goodbye!");
    }
}
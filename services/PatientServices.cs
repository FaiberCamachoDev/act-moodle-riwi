using models;
using helpers;

namespace services;

public class PatientService
{
    private List<Patient> _patientsDatabase;

    public PatientService()
    {
        _patientsDatabase = new List<Patient>();
    }

    public bool RegisterPatient()
    {
        UIHelpers.PrintTitle("Register Patient & Pet");
        UIHelpers.PrintDescription("Please fill in the patient's information below");

        Patient newPatient = new Patient();
        newPatient.Id = _patientsDatabase.Count > 0 ? _patientsDatabase.Max(p => p.Id) + 1 : 1;

        // --- DATOS DEL PACIENTE ---
        do
        {
            Write("Patient Name: ");
            newPatient.Name = ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrEmpty(newPatient.Name))
            {
                UIHelpers.PrintError("Name cannot be empty.");
            }
        } while (string.IsNullOrEmpty(newPatient.Name));

        bool isAgeValid = false;
        while (!isAgeValid)
        {
            Write("Age: ");
            string ageInput = ReadLine() ?? "";
            
            if (int.TryParse(ageInput, out int parsedAge))
            {
                if (parsedAge < 0)
                {
                    UIHelpers.PrintError("Age cannot be a negative number.");
                }
                else
                {
                    newPatient.Age = parsedAge;
                    isAgeValid = true;
                }
            }
            else
            {
                UIHelpers.PrintError("Please enter a valid integer number for the age.");
            }
        }
        Write("Phone: "); 
        newPatient.Phone = ReadLine()?.Trim() ?? "Not specified";
        
        Write("Main Symptom: ");
        newPatient.Symptom = ReadLine()?.Trim() ?? "Not specified";

        // --- DATOS DE LA MASCOTA ---
        UIHelpers.PrintDescription("Pet Information");
        Pet newPet = new Pet();

        do
        {
            Write("Pet Name: ");
            newPet.Name = ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrEmpty(newPet.Name))
            {
                UIHelpers.PrintError("Pet Name cannot be empty.");
            }
        } while (string.IsNullOrEmpty(newPet.Name));

        Write("Species (e.g., Dog, Cat): ");
        newPet.Species = ReadLine()?.Trim() ?? "Not specified";

        Write("Breed: ");
        newPet.Breed = ReadLine()?.Trim() ?? "Not specified";

        // VINCULACIÓN: Le asignamos la mascota recién creada al paciente
        newPatient.PatientPet = newPet;

        // Guardamos todo en la base de datos
        _patientsDatabase.Add(newPatient);
        UIHelpers.PrintSuccess($"Patient '{newPatient.Name}' and pet '{newPet.Name}' registered successfully!");
        
        Pause();
        return false;
    }

    public bool ListPatients()
    {
        UIHelpers.PrintTitle("Patients & Pets List");
        
        if (_patientsDatabase.Count == 0)
        {
            UIHelpers.PrintInfo("No records in the system yet.");
        }
        else
        {
            UIHelpers.PrintDescription("Current registered records in the database");
            
            foreach (var patient in _patientsDatabase)
            {
                WriteLine($"[{patient.Id}] Owner: {patient.Name} ({patient.Age} yrs) | Symptom: {patient.Symptom}");
                // Mostramos los datos de la mascota (PatientPet)
                WriteLine($"      Pet: {patient.PatientPet.Name} | Species: {patient.PatientPet.Species} | Breed: {patient.PatientPet.Breed}");
                WriteLine("--------------------------------------------------");
            }
        }
        
        Pause();
        return false;
    }

    public bool SearchPatient()
    {
        UIHelpers.PrintTitle("Search Record");
        UIHelpers.PrintDescription("Search by exact or partial owner's name");
        
        Write("Enter the owner's name to search: ");
        string searchName = ReadLine()?.Trim() ?? "";

        var foundPatient = _patientsDatabase.FirstOrDefault(p => 
            p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (foundPatient != null)
        {
            UIHelpers.PrintSuccess("Record Found!");
            WriteLine($"Owner ID: {foundPatient.Id}");
            WriteLine($"Name: {foundPatient.Name} | Age: {foundPatient.Age}");
            WriteLine($"Symptom: {foundPatient.Symptom}");
            WriteLine($"Pet Name: {foundPatient.PatientPet.Name}");
            WriteLine($"Pet Species: {foundPatient.PatientPet.Species} | Breed: {foundPatient.PatientPet.Breed}");
        }
        else
        {
            UIHelpers.PrintInfo($"No record found under the name '{searchName}'.");
        }

        Pause();
        return false;
    }

    public bool ShowError()
    {
        UIHelpers.PrintError("Invalid option. Please choose a valid number from the menu.");
        Pause();
        return false;
    }

    private void Pause()
    {
        WriteLine("\nPress ENTER to continue...");
        ReadLine();
    }
    // este metodo devuelve la lista para que otros puedan leerla, pero manteniendo el control.
    public List<Patient> GetPatientsDatabase()
    {
        return _patientsDatabase;
    }
}
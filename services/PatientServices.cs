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

    public List<Patient> GetPatientsDatabase()
    {
        return _patientsDatabase;
    }

    public bool RegisterPatient()
    {
        UIHelpers.PrintTitle("Register Patient & Pets");

        Patient newPatient = new Patient();
        newPatient.Id = _patientsDatabase.Count > 0 ? _patientsDatabase.Max(p => p.Id) + 1 : 1;

        Write("Patient Name: ");
        newPatient.Name = ReadLine()?.Trim() ?? "";

        Write("Age: ");
        if (int.TryParse(ReadLine(), out int parsedAge)) newPatient.Age = parsedAge;

        Write("Phone: ");
        newPatient.Phone = ReadLine()?.Trim() ?? "";

        Write("Address: ");
        newPatient.Address = ReadLine()?.Trim() ?? "";

        Write("Main Symptom: ");
        newPatient.Symptom = ReadLine()?.Trim() ?? "Not specified";

        // --- REGISTRO MÚLTIPLE DE MASCOTAS ---
        bool addMorePets = true;
        do
        {
            UIHelpers.PrintDescription($"Adding Pet #{newPatient.OwnedPets.Count + 1}");
            
            Write("Pet Name: ");
            string petName = ReadLine()?.Trim() ?? "Unknown";

            Write("Age: ");
            int.TryParse(ReadLine(), out int petAge);

            Write("Species (Dog, Cat, Bird, etc.): ");
            string species = ReadLine()?.Trim() ?? "Unknown";

            Write("Breed: ");
            string breed = ReadLine()?.Trim() ?? "Unknown";

            // Usamos el nuevo constructor de Pet
            Pet newPet = new Pet(petName, petAge, species, breed);
            newPatient.OwnedPets.Add(newPet);

            Write("\nDo you want to add another pet for this patient? (y/n): ");
            string answer = ReadLine()?.Trim().ToLower() ?? "n";
            if (answer != "y")
            {
                addMorePets = false;
            }

        } while (addMorePets);

        _patientsDatabase.Add(newPatient);
        UIHelpers.PrintSuccess($"Patient '{newPatient.Name}' registered with {newPatient.OwnedPets.Count} pet(s).");
        
        Pause();
        return false;
    }

    public bool ListPatients()
    {
        UIHelpers.PrintTitle("Patients & Pets List");
        
        if (_patientsDatabase.Count == 0)
        {
            UIHelpers.PrintInfo("No records in the system yet.");
            Pause();
            return false;
        }

        foreach (var patient in _patientsDatabase)
        {
            // Usamos el método de la interfaz IRegisterable
            patient.DisplayInformation();
            
            foreach (var pet in patient.OwnedPets)
            {
                // Aquí aplicamos Polimorfismo. Automáticamente ejecutará MakeSound() de Pet.
                pet.DisplayInformation(); 
            }
            WriteLine("--------------------------------------------------");
        }
        
        Pause();
        return false;
    }

    public bool SearchPatient()
    {
        UIHelpers.PrintTitle("Search Record");
        Write("Enter the owner's name to search: ");
        string searchName = ReadLine()?.Trim() ?? "";

        var foundPatient = _patientsDatabase.FirstOrDefault(p => 
            p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

        if (foundPatient != null)
        {
            UIHelpers.PrintSuccess("Record Found!");
            foundPatient.DisplayInformation();
            foreach (var pet in foundPatient.OwnedPets)
            {
                pet.DisplayInformation();
            }
        }
        else
        {
            UIHelpers.PrintInfo($"No record found under the name '{searchName}'.");
        }

        Pause();
        return false;
    }

    // Nuevo método para probar las Clases Abstractas
    public bool AttendPatient()
    {
        UIHelpers.PrintTitle("Attend Patient");
        Write("Enter patient ID to attend: ");
        
        if (int.TryParse(ReadLine(), out int id))
        {
            var patient = _patientsDatabase.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                // Usamos Polimorfismo con clases abstractas
                VeterinaryService service;
                
                Write("Select service (1. General Consultation, 2. Vaccination): ");
                string choice = ReadLine()?.Trim() ?? "1";

                if (choice == "2") service = new Vaccination();
                else service = new GeneralConsultation();

                service.Attend(patient);
            }
            else
            {
                UIHelpers.PrintError("Patient not found.");
            }
        }
        Pause();
        return false;
    }

    public bool ShowError()
    {
        UIHelpers.PrintError("Invalid option.");
        Pause();
        return false;
    }

    private void Pause()
    {
        WriteLine("\nPress ENTER to continue...");
        ReadLine();
    }
}
using models;
using helpers;

namespace services;

public class ClinicalReports
{
    public void DemonstrateDictionaryUsage(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Dictionary Fast Lookup");
        Dictionary<int, Patient> patientsDictionary = patientsList.ToDictionary(p => p.Id, p => p);

        Write("Enter Patient ID for instant lookup: ");
        if (int.TryParse(ReadLine(), out int searchId))
        {
            if (patientsDictionary.TryGetValue(searchId, out Patient? foundPatient))
            {
                UIHelpers.PrintSuccess($"Patient Found instantly! Name: {foundPatient.Name}");
            }
            else
            {
                UIHelpers.PrintError($"No patient exists with ID {searchId}.");
            }
        }
        Pause();
    }

    public void ShowDogOwnersOrderedByAge(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Dog Owners Report (Method Syntax)");

        // FIX: Usamos .Any() para buscar si DENTRO de la lista de mascotas hay algún perro
        var dogOwnersReport = patientsList
            .Where(p => p.OwnedPets.Any(pet => pet.Species.Equals("Dog", StringComparison.OrdinalIgnoreCase)))
            .OrderBy(p => p.Age)
            .Select(p => new { 
                OwnerName = p.Name, 
                OwnerPhone = p.Phone, 
                OwnerAge = p.Age 
            })
            .ToList();

        if (dogOwnersReport.Any())
        {
            foreach (var record in dogOwnersReport)
            {
                WriteLine($"- Owner: {record.OwnerName} | Phone: {record.OwnerPhone} | Age: {record.OwnerAge}");
            }
        }
        else
        {
            UIHelpers.PrintInfo("No dog owners found.");
        }
        Pause();
    }

    public void ShowPatientsGroupedByPetSpecies(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Demographics: Patients by Pet Species");

        // FIX: Expandimos la lista de mascotas con SelectMany para poder agruparlas
        var groupedPets = patientsList
            .SelectMany(p => p.OwnedPets, (patient, pet) => new { patient.Name, pet.Species, pet.Breed })
            .GroupBy(x => x.Species);

        foreach (var group in groupedPets)
        {
            WriteLine($"\nSpecies: {group.Key.ToUpper()} (Total: {group.Count()})");
            foreach (var item in group)
            {
                WriteLine($"   -> Owner: {item.Name} | Breed: {item.Breed}");
            }
        }
        Pause();
    }

    public void SolvePracticalProblems(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Advanced Analytics (Practical Problems)");

        if (!patientsList.Any())
        {
            UIHelpers.PrintError("Not enough data to run analytics.");
            Pause();
            return;
        }

        var youngest = patientsList.OrderBy(p => p.Age).First();
        var oldest = patientsList.OrderByDescending(p => p.Age).First();
        
        WriteLine($"[Info] Youngest Patient: {youngest.Name} ({youngest.Age} yrs)");
        WriteLine($"[Info] Oldest Patient: {oldest.Name} ({oldest.Age} yrs)\n");

        bool hasUnknownBreeds = patientsList.Any(p => 
            p.OwnedPets.Any(pet => string.IsNullOrWhiteSpace(pet.Breed) || pet.Breed == "Not specified"));
        
        WriteLine($"[Info] Are there any pets with unknown breeds? {(hasUnknownBreeds ? "YES" : "NO")}\n");

        WriteLine("[Info] All patient names (Alphabetical, Uppercase):");
        var uppercaseNames = patientsList
            .Select(p => p.Name.ToUpper())
            .OrderBy(name => name)
            .ToList();

        foreach (var name in uppercaseNames)
        {
            WriteLine($"  - {name}");
        }

        Pause();
    }

    private void Pause()
    {
        WriteLine("\nPress ENTER to continue...");
        ReadLine();
    }
}
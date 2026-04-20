// File: services/ClinicalReports.cs
using System;
using System.Collections.Generic;
using System.Linq;
using models;
using helpers;

namespace services;

public class ClinicalReports
{
    // TASK 1: Uso de Diccionarios
    // Convertimos la lista en un Dictionary para búsquedas ultra rápidas por ID.
    public void DemonstrateDictionaryUsage(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Dictionary Fast Lookup");
        
        // Convertimos List<Patient> a Dictionary<int, Patient> donde la llave es el Id
        Dictionary<int, Patient> patientsDictionary = patientsList.ToDictionary(p => p.Id, p => p);

        Write("Enter Patient ID for instant lookup: ");
        if (int.TryParse(ReadLine(), out int searchId))
        {
            // TryGetValue forma para buscar en un diccionario
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

    // TASK 2 & 4: Sintaxis de Métodos, Encadenamiento y Tipos Anónimos (Select)
    public void ShowDogOwnersOrderedByAge(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Dog Owners Report (Method Syntax)");

        // LINQ Chain: Where -> OrderBy -> Select
        var dogOwnersReport = patientsList
            .Where(p => p.PatientPet != null && p.PatientPet.Species.Equals("Dog", StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => p.Age)
            .Select(p => new { 
                OwnerName = p.Name, 
                OwnerPhone = p.Phone, 
                DogAge = p.Age 
            }) // Proyección: Creamos un objeto "anónimo" solo con los datos requeridos
            .ToList();

        if (dogOwnersReport.Any()) // Método Any() para verificar si hay resultados
        {
            foreach (var record in dogOwnersReport)
            {
                WriteLine($"- Owner: {record.OwnerName} | Phone: {record.OwnerPhone} | Age: {record.DogAge}");
            }
        }
        else
        {
            UIHelpers.PrintInfo("No dog owners found.");
        }
        Pause();
    }

    // TASK 2 & 5: GroupBy y Conteo
    public void ShowPatientsGroupedByPetSpecies(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Demographics: Patients by Pet Species");

        // Sintaxis de Consulta (Query Syntax).
        var groupedPatients = from p in patientsList
                              where p.PatientPet != null
                              group p by p.PatientPet.Species into speciesGroup
                              select speciesGroup;
        foreach (var group in groupedPatients)
        {
            // group.Key es la especie (ej. "Cat"). group.Count() cuenta cuántos hay.
            WriteLine($"\nSpecies: {group.Key.ToUpper()} (Total: {group.Count()})");
            foreach (var patient in group)
            {
                WriteLine($"   -> Owner: {patient.Name} | Pet Name: {patient.PatientPet!.Name}");
            }
        }
        Pause();
    }

    // TASK 5: Problemas Prácticos (Min, Max, Any, Select, OrderBy)
    public void SolvePracticalProblems(List<Patient> patientsList)
    {
        UIHelpers.PrintTitle("Advanced Analytics (Practical Problems)");

        if (!patientsList.Any())
        {
            UIHelpers.PrintError("Not enough data to run analytics.");
            Pause();
            return;
        }

        // 1. Paciente más joven y más viejo (Usando OrderBy y First/Last)
        var youngest = patientsList.OrderBy(p => p.Age).First();
        var oldest = patientsList.OrderByDescending(p => p.Age).First();
        
        WriteLine($"[Info] Youngest Patient: {youngest.Name} ({youngest.Age} yrs)");
        WriteLine($"[Info] Oldest Patient: {oldest.Name} ({oldest.Age} yrs)\n");

        // 2. Verificar condición con Any (Mascotas sin raza)
        bool hasUnknownBreeds = patientsList.Any(p => 
            p.PatientPet != null && 
            (string.IsNullOrWhiteSpace(p.PatientPet.Breed) || p.PatientPet.Breed == "Not specified"));
        
        WriteLine($"[Info] Are there any pets with unknown breeds? {(hasUnknownBreeds ? "YES" : "NO")}\n");
        // 3. Nombres en mayúsculas ordenados alfabéticamente
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
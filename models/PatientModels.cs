using modules_activities.Interfaces;

namespace models;

public class Patient : IRegisterable, INotifications
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; } // Nuevo atributo (Task 2)
    public string Symptom { get; set; }
    
    // Task 3: Ahora es una lista para soportar múltiples mascotas
    public List<Pet> OwnedPets { get; set; }

    // Constructor para inicializar la lista y evitar nulos
    public Patient()
    {
        Name = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Symptom = string.Empty;
        OwnedPets = new List<Pet>(); 
    }

    // Cumpliendo el contrato de la interfaz
    public void DisplayInformation()
    {
        Console.WriteLine($"[Patient ID: {Id}] {Name} | Phone: {Phone} | Address: {Address}");
        Console.WriteLine($"Main Symptom: {Symptom}");
        Console.WriteLine($"Total Pets Owned: {OwnedPets.Count}");
    }

    public void SendNotifications()
    {
        WriteLine($"[Notification to {Name}]: Hi, Remember your pet's appointment tomorrow");
    }
}
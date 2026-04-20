


namespace models;

public class Pet : Animal, IRegisterable
{
    public string Breed { get; set; }

    // El constructor 'base' envía los datos requeridos a la clase padre (Animal)
    public Pet(string name, int age, string species, string breed) 
        : base(name, age, species)
    {
        Breed = breed;
    }

    // Polimorfismo: Sobreescribimos el comportamiento original
    public override string MakeSound()
    {
        if (Species.Equals("Dog", StringComparison.OrdinalIgnoreCase)) return "Woof!";
        if (Species.Equals("Cat", StringComparison.OrdinalIgnoreCase)) return "Meow!";
        if (Species.Equals("Bird", StringComparison.OrdinalIgnoreCase)) return "Tweet!";
        
        return base.MakeSound(); // Si no es ninguno, usa el sonido genérico de la clase padre
    }

    // Cumpliendo el contrato de la interfaz IRegisterable
    public void DisplayInformation()
    {
        Console.WriteLine($"[Pet] {Name} | {Species} ({Breed}) | {Age} yrs | Sound: {MakeSound()}");
    }
}
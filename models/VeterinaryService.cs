using modules_activities.Interfaces;

namespace models;

public abstract class VeterinaryService: IAtendible
{
    public string ServiceName { get; set; }
    public decimal Cost { get; set; }

    protected VeterinaryService(string name, decimal cost)
    {
        ServiceName = name;
        Cost = cost;
    }

    // Metodo abstracto > Obliga a las clases hijas a implementarlo
    public abstract void Attend(Patient patient);
    
}

using helpers;

namespace models;

// Primera subclase: Consulta General
public class GeneralConsultation : VeterinaryService
{
    // El constructor usa "base" para enviar el nombre y costo al padre
    public GeneralConsultation() : base("General Consultation", 50.00m) { }

    // Sobreescribimos el método abstracto obligatorio
    public override void Attend(Patient patient)
    {
        UIHelpers.PrintSuccess($"Starting {ServiceName} for patient {patient.Name}.");
        WriteLine($"Reviewing main symptom: {patient.Symptom}");
        WriteLine($"Total cost will be: ${Cost}");
    }
}

// Segunda subclase: Vacunación
public class Vaccination : VeterinaryService
{
    public Vaccination() : base("Annual Vaccination", 35.00m) { }

    public override void Attend(Patient patient) // parametros Patient para poder usar las propiedades en las ecuaciones matematicas.
    {
        UIHelpers.PrintSuccess($"Starting {ServiceName} for patient {patient.Name}.");
        WriteLine($"Applying vaccines to {patient.OwnedPets.Count} pets.");
        WriteLine($"Total cost will be: ${Cost * patient.OwnedPets.Count}");
    }
}
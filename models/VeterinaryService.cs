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
// week 5 
// TASK 1: Documentación requerida en el código
/*
 * DECISIONES DE DISEÑO - PROGRAMACIÓN ASÍNCRONA:
 * Usamos async y await porque guardar un paciente en la base de datos
 * es una operación de Entrada/Salida (I/O). Si lo hiciéramos de forma sincrona,
 * la pantalla del usuario se congelaria. Con async, liberamos el hilo principal
 * para que la app siga repsondiendo a otros users
 */
public class ClinicManager
{
    //task 2: regla oro devuelve siempre Task y Async
    public async Task RegisterPatientAsync(string userName)
    {
        Console.WriteLine($"[1. start] Starting to register {userName} on db...");

        // Simulamos que el disco duro tarda 3 segundos en guardar la información.
        // El 'await' le dice al programa: Pausa esta función aquí y ve a hacer otras cosas mientras espera
        await Task.Delay(3000); 

        Console.WriteLine($"[3. end] ¡Register {userName} successfully!");
    }
}
/* * DECISIONES DE DISEÑO - SEMANA 4:
 * * 1. Clases Abstractas: Se usan para 'VeterinaryService' y 'Animal' porque 
 * comparten una identidad base y propiedades comunes (ej. Cost, Name). Define "qué ES" el objeto.
 * * 2. Interfaces: Se usarán 'IRegistrable', 'IAtendible' e 'INotificable' porque 
 * definen "qué PUEDE HACER" el objeto. Esto nos da flexibilidad: un Paciente y una Mascota 
 * no comparten la misma clase base, pero ambos comparten la capacidad de ser registrados (IRegistrable).
 */

namespace models;

public abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Species { get; set; }

    public Animal(string name, int age, string species)
    {
        Name = name;
        Age = age;
        Species = species;
    }

    // El modificador "virutal" permite que las clases hijas sobreescriban este método (Polimorfismo)
    public virtual string MakeSound()
    {
        return "Generic animal sound";
    }
}
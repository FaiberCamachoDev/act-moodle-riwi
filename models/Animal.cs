using System;

namespace models;

public class Animal
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
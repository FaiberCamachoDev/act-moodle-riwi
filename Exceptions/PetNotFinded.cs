namespace modules_activities.Exceptions;

public class PetNotFinded : Exception
{
    // se crea el contructor para q le pase el mensaje a la clase padre
    public PetNotFinded(string message) : base(message)
    {
    }
}
namespace Enter.ENB.Exceptions;

public class EntInitializationException : EntException
{
    public EntInitializationException(string? message) : base(message)
    {
    }

    public EntInitializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
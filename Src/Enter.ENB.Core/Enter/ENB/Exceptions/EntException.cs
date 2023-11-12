using System.Runtime.Serialization;

namespace Enter.ENB.Exceptions;

public class EntException : Exception
{
    public EntException()
    {
    }

    public EntException(string? message)
        : base(message)
    {
    }

    public EntException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public EntException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}
namespace Domain.Exceptions;

public class AlreadyFoundException : BaseException
{
    private static string message = "Same instance was already found.";

    public AlreadyFoundException() : base(message)
    {
    }
}
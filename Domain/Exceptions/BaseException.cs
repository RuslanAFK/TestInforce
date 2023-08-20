using System.Net;

namespace Domain.Exceptions;

public class BaseException : Exception
{
    public override string Message { get; }
    public virtual HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

    public BaseException(string message)
    {
        Message = message;
    }

}

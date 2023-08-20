using System.Net;

namespace Domain.Exceptions;

public class UserNotAuthorizedException : BaseException
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.Unauthorized;
    private static string message = "You are not authorized.";
    public UserNotAuthorizedException() : base(message)
    {
    }
}
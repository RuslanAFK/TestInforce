using System.Net;

namespace Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        private static string message = "Not found.";
        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

        public NotFoundException() : base(message)
        {
        }
    }
}
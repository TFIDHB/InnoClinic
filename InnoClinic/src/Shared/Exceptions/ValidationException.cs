using System.Net;

namespace InnoClinic.Shared.Exceptions
{
    public class ValidationException : BasicException
    {
        public ValidationException(string message) : base(message, HttpStatusCode.BadRequest) { }
    }
}

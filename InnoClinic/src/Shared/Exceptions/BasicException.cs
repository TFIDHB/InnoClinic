using System.Net;

namespace InnoClinic.Shared.Exceptions
{
    public abstract class BasicException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected BasicException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

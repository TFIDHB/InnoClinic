namespace InnoClinic.Shared.Exceptions
{
    public class ExternalServiceException : Exception
    {
        public ExternalServiceException(string serviceName) : base(string.Format(SharedMessages.ExternalServiceMessage, serviceName))
        {
        }
    }
}

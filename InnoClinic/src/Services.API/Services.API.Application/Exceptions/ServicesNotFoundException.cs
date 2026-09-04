using InnoClinic.Shared.Exceptions;

namespace Application.Exceptions
{
    public class ServicesNotFoundException : NotFoundException
    {
        public ServicesNotFoundException(string missingIds)
            : base(string.Format(ServiceMessages.ServicesNotFoundMessage, missingIds))
        {
        }
    }
}

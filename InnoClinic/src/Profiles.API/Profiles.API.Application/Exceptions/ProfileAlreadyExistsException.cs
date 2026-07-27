using InnoClinic.Shared.Exceptions;

namespace Application.Exceptions
{
    public class ProfileAlreadyExistsException : BadRequestException
    {
        public ProfileAlreadyExistsException() : base(ProfilesMessages.ProfileAlreadyExistsMessage)
        {
        }
    }
}

namespace BLL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base(BllMessages.UserNotFound) { }
    }
}

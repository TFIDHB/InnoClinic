using InnoClinic.Shared.Interfaces;

namespace DAL.Interfaces
{
    public interface IAuthUnitOfWork : IBasicUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}

using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IOfficesUnitOfWork : IBasicUnitOfWork
    {
        IOfficesRepository OfficesRepository { get; }
    }
}

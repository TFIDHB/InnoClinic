using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IServicesUnitOfWork : IBasicUnitOfWork
    {
        IServicesRepository ServicesRepository { get; }
        ISpecializationsRepository SpecializationsRepository { get; }
    }
}

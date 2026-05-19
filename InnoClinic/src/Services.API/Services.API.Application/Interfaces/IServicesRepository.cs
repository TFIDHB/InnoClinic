using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IServicesRepository: IRepository<Service, Guid>
    {
    }
}

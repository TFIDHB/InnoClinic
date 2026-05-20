using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class ServicesRepository(ServicesDbContext context) : BaseRepository<Service, Guid>(context), IServicesRepository
    {
    }
}

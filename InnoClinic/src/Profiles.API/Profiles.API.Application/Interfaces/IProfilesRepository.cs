using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IProfilesRepository : IRepository<DoctorProfile, Guid>
    {
        //future functionality according to US
    }
}

using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IProfilesRepository : IRepository<Profile, Guid>
    {
        //future functionality according to US
    }
}

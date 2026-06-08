using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IPatientProfilesRepository : IRepository<PatientProfile, Guid>
    {
        //future functionality according to US
    }
}

using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IProfilesUnitOfWork : IBasicUnitOfWork
    {
        IDoctorProfilesRepository DoctorProfilesRepository { get; }
        IPatientProfilesRepository PatientProfilesRepository { get; }
        IReceptionistProfilesRepository ReceptionistProfilesRepository { get; }
    }
}

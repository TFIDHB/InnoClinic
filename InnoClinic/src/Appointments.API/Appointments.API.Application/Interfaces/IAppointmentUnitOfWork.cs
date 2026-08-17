using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IAppointmentUnitOfWork : IBasicUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        IResultRepository ResultRepository { get; }
    }
}

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        Task SaveChangesAsync();
    }
}

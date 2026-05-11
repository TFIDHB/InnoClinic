using Domain.Entities;
using InnoClinic.Shared.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
    }
}

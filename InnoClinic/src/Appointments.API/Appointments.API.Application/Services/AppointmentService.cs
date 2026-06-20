using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class AppointmentService(IAppointmentUnitOfWork unitOfWork, IMapper mapper, IServicesClient servicesClient) : IAppointmentService
    {
        public async Task<AppointmentResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Appointment));

            return mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto, CancellationToken ct = default)
        {
            var timeSlotSize = await servicesClient.GetTimeSlotSizeAsync(dto.ServiceId, ct);
            var durationMinutes = timeSlotSize * 10;
            var newStart = dto.Time;
            var newEnd = dto.Time.AddMinutes(durationMinutes);

            var isOverlapping = await unitOfWork.AppointmentRepository.AnyAsync(a =>
                a.DoctorId == dto.DoctorId &&
                a.Date == dto.Date &&
                newStart < a.Time.Add(a.Duration) &&
                newEnd > a.Time, ct);

            if (isOverlapping)
                throw new OverlappingAppointmentException();

            var appointment = mapper.Map<Appointment>(dto);
            appointment.Duration = TimeSpan.FromMinutes(durationMinutes);

            await unitOfWork.AppointmentRepository.CreateAsync(appointment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentSlotDto>> GetSlotsByDateAndDoctorAsync(DateOnly date, Guid? doctorId, CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetByDateAndDoctorAsync(date, doctorId, ct);
            return mapper.Map<IEnumerable<AppointmentSlotDto>>(appointments);
        }
    }
}

using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Clients;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class AppointmentService(
        IAppointmentUnitOfWork unitOfWork,
        IMapper mapper,
        IServicesClient servicesClient,
        IProfilesClient profilesClient) : IAppointmentService
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

        public async Task<IEnumerable<AppointmentSlotDto>> GetSlotsByDateAndDoctorAsync(
            DateOnly date,
            Guid? doctorId,
            CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetByDateAndDoctorAsync(date, doctorId, ct);
            return mapper.Map<IEnumerable<AppointmentSlotDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentSlotDto>> GetSlotsByDateRangeAndDoctorAsync(
            DateOnly startDate,
            DateOnly endDate,
            Guid? doctorId,
            CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetByDateRangeAndDoctorAsync(startDate, endDate, doctorId, ct);
            return mapper.Map<IEnumerable<AppointmentSlotDto>>(appointments);
        }

        public async Task<IEnumerable<ScheduleDto>> GetDoctorAppointmentScheduleAsync(
            Guid doctorId,
            DateOnly date,
            CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetByDateAndDoctorAsync(date, doctorId, ct);
            var orderedAppointments = appointments.OrderBy(e => e.Time).ToList();

            var appointmentsList = new List<ScheduleDto>(orderedAppointments.Count);

            foreach (var appointment in orderedAppointments) 
            {
                var serviceName = await servicesClient.GetServiceNameAsync(appointment.ServiceId, ct);
                var patientInfo = await profilesClient.GetPatientInfoAsync(appointment.PatientId, ct);

                var entry = mapper.Map<ScheduleDto>(appointment);

                entry.PatientFullName = patientInfo == null 
                    ? "Unknown patient" 
                    : $"{patientInfo.LastName} {patientInfo.FirstName} {patientInfo.MiddleName}".Trim();
                entry.ServiceName = serviceName ?? "Unknown service";

                appointmentsList.Add(entry);
            }

            return appointmentsList;
        }
    }
}

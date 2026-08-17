using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Clients;
using InnoClinic.Shared.Exceptions;
using System.Numerics;

namespace Application.Services
{
    public class AppointmentService(
        IAppointmentUnitOfWork unitOfWork,
        IMapper mapper,
        IServicesClient servicesClient,
        IProfilesClient profilesClient) : IAppointmentService
    {
        private async Task<bool> IsOverlappingAsync(
            Guid doctorId,
            DateOnly date,
            TimeOnly newStart,
            TimeOnly newEnd,
            Guid? excludeAppointmentId,
            CancellationToken ct)
        {
            return await unitOfWork.AppointmentRepository.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.Date == date &&
                (excludeAppointmentId == null || a.Id != excludeAppointmentId.Value) &&
                newStart < a.Time.Add(a.Duration) &&
                newEnd > a.Time, ct);
        }

        public async Task<AppointmentResponseDto> GetByIdAsync(
            Guid appointmentId,
            Guid? patientId,
            Guid? doctorId,
            CancellationToken ct = default)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId, ct)
                ?? throw new NotFoundException(nameof(Appointment));

            if (patientId.HasValue && patientId.Value != appointment.PatientId)
                throw new ForbiddenException(AppointmentsApiMessages.ForbiddenAccessMessage);

            if (doctorId.HasValue && doctorId.Value != appointment.DoctorId)
                throw new ForbiddenException(AppointmentsApiMessages.ForbiddenAccessMessage);

            return mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto, CancellationToken ct = default)
        {
            var timeSlotSize = await servicesClient.GetTimeSlotSizeAsync(dto.ServiceId, ct);
            var durationMinutes = timeSlotSize * 10;
            var newStart = dto.Time;
            var newEnd = dto.Time.AddMinutes(durationMinutes);

            var isOverlapping = await IsOverlappingAsync(dto.DoctorId, dto.Date, newStart, newEnd, excludeAppointmentId: null, ct);
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

        public async Task<IEnumerable<AppointmentListItemDto>> GetFilteredAppointmentsAsync(
            DateOnly? date,
            Guid? officeId,
            bool? isApproved,
            string? doctorFullName,
            string? serviceName,
            CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetFilteredAsync(date, officeId, isApproved, ct);
            var enrichedAppointments = new List<AppointmentListItemDto>();

            foreach (var appointment in appointments)
            {
                var doctorInfo = await profilesClient.GetDoctorInfoAsync(appointment.DoctorId, ct);
                var patientInfo = await profilesClient.GetPatientInfoAsync(appointment.PatientId, ct);
                var service = await servicesClient.GetServiceNameAsync(appointment.ServiceId, ct);

                var entry = mapper.Map<AppointmentListItemDto>(appointment);
                entry.DoctorFullName = doctorInfo == null
                    ? "Unknown doctor"
                    : $"{doctorInfo.LastName} {doctorInfo.FirstName} {doctorInfo.MiddleName}".Trim();
                entry.PatientFullName = patientInfo == null
                    ? "Unknown patient"
                    : $"{patientInfo.LastName} {patientInfo.FirstName} {patientInfo.MiddleName}".Trim();
                entry.PatientPhoneNumber = patientInfo?.PhoneNumber;
                entry.ServiceName = service ?? "Unknown service";

                enrichedAppointments.Add(entry);
            }

            if (!string.IsNullOrWhiteSpace(doctorFullName))
                enrichedAppointments = enrichedAppointments.Where(e => e.DoctorFullName.Contains(doctorFullName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(serviceName))
                enrichedAppointments = enrichedAppointments.Where(e => e.ServiceName.Contains(serviceName, StringComparison.OrdinalIgnoreCase)).ToList();

            return enrichedAppointments
                .OrderBy(e => e.StartTime)
                .ThenBy(e => e.DoctorFullName, StringComparer.OrdinalIgnoreCase)
                .ThenBy(e => e.ServiceName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public async Task ApproveAsync(Guid id, CancellationToken ct = default)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Appointment));

            appointment.IsApproved = true;

            await unitOfWork.AppointmentRepository.UpdateAsync(appointment, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task CancelAsync(Guid id, CancellationToken ct = default)
        {
            await unitOfWork.AppointmentRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<AppointmentHistoryItemDto>> GetPatientHistoryAsync(Guid patientId, CancellationToken ct = default)
        {
            var appointments = await unitOfWork.AppointmentRepository.GetByPatientAsync(patientId, ct);

            var appointmentsList = new List<AppointmentHistoryItemDto>();

            foreach (var appointment in appointments)
            {
                var doctorInfo = await profilesClient.GetDoctorInfoAsync(appointment.DoctorId, ct);
                var serviceName = await servicesClient.GetServiceNameAsync(appointment.ServiceId, ct);

                var entry = mapper.Map<AppointmentHistoryItemDto>(appointment);
                entry.DoctorFullName = doctorInfo == null
                    ? "Unknown doctor"
                    : $"{doctorInfo.LastName} {doctorInfo.FirstName} {doctorInfo.MiddleName}".Trim();
                entry.ServiceName = serviceName ?? "Unknown service";
                entry.HasResult = false;
                entry.CanReschedule = appointment.Date > DateOnly.FromDateTime(DateTime.UtcNow) ||
                    (appointment.Date == DateOnly.FromDateTime(DateTime.UtcNow) && appointment.Time > TimeOnly.FromDateTime(DateTime.UtcNow));

                appointmentsList.Add(entry);
            }

            return appointmentsList;
        }

        public async Task<AppointmentResponseDto> RescheduleAsync(
            Guid appointmentId,
            RescheduleAppointmentRequestDto dto,
            Guid? patientId,
            CancellationToken ct = default)
        {
            var appointment = await unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId, ct)
                ?? throw new NotFoundException(nameof(Appointment));

            if (patientId.HasValue && appointment.PatientId != patientId.Value)
                throw new ForbiddenException(AppointmentsApiMessages.ForbiddenAccessMessage);

            if (appointment.IsApproved)
                throw new BadRequestException(AppointmentsApiMessages.CannotBeRescheduledMessage);

            var newStart = dto.Time;
            var newEnd = dto.Time.Add(appointment.Duration);

            var isOverlapping = await IsOverlappingAsync(dto.DoctorId, dto.Date, newStart, newEnd, appointmentId, ct);
            if (isOverlapping)
                throw new OverlappingAppointmentException();

            var doctorInfo = await profilesClient.GetDoctorInfoAsync(dto.DoctorId, ct)
                ?? throw new NotFoundException("Doctor");
            appointment.DoctorId = dto.DoctorId;
            appointment.OfficeId = doctorInfo.OfficeId;
            appointment.Date = dto.Date;
            appointment.Time = dto.Time;

            await unitOfWork.AppointmentRepository.UpdateAsync(appointment, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<AppointmentResponseDto>(appointment);
        }
    }
}

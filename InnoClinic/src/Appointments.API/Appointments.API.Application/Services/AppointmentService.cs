using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class AppointmentService(IAppointmentUnitOfWork unitOfWork, IMapper mapper) : IAppointmentService
    {
        private readonly IAppointmentUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<AppointmentResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Appointment));

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }
        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto, CancellationToken ct = default)
        {
            var newStart = dto.Time;
            var newEnd = dto.Time.AddMinutes(dto.DurationMinutes);

            var isOverlapping = await _unitOfWork.AppointmentRepository.AnyAsync(a =>
                a.DoctorId == dto.DoctorId &&
                a.Date == dto.Date &&
                newStart < a.Time.Add(a.Duration) &&
                newEnd > a.Time, ct);

            if (isOverlapping)
            {
                throw new OverlappingAppointmentException();
            }

            var appointment = _mapper.Map<Appointment>(dto);

            appointment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.AppointmentRepository.CreateAsync(appointment, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }
    }
}

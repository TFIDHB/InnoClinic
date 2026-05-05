using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class AppointmentService(IAppointmentUnitOfWork unitOfWork, IMapper mapper) : IAppointmentService
    {
        private readonly IAppointmentUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto)
        {
            var newStart = dto.Time;
            var newEnd = dto.Time.AddMinutes(dto.DurationMinutes);

            var isOverlapping = await _unitOfWork.AppointmentRepository.AnyAsync(a =>
                a.DoctorId == dto.DoctorId &&
                a.Date == dto.Date &&
                newStart < a.Time.Add(a.Duration) &&
                newEnd > a.Time);

            if (isOverlapping)
            {
                throw new OverlappingAppointmentException();
            }

            var appointment = _mapper.Map<Appointment>(dto);

            appointment.Status = AppointmentStatus.Created;
            appointment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }
    }
}

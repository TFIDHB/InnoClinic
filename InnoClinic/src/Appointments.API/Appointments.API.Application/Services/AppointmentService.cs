using Application.DTOs;
using Application.Exceptions;
using Application.Extensions;
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
            var durationMinutes = dto.ServiceType.GetRequiredSlots() * 10;
            var newEnd = dto.Time.AddMinutes(durationMinutes);

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

            await _unitOfWork.AppointmentRepository.CreateAsync(appointment, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }
        public async Task<IEnumerable<TimeOnly>> GetAvailableSlotsAsync(GetAvailableSlotsRequestDto dto, CancellationToken ct = default)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetByDateAndDoctorAsync(dto.Date, dto.DoctorId, ct);

            var busySlots = GetBusySlots(appointments);

            var requiredSlots = dto.ServiceType.GetRequiredSlots();
            var allSlots = GenerateAllSlots(requiredSlots);

            return allSlots
                .Where(slot => Enumerable
                    .Range(0, requiredSlots)
                    .Select(i => slot.AddMinutes(i * 10))
                    .All(s => !busySlots.Contains(s)));
        }
        public async Task<IEnumerable<DateOnly>> GetAvailableDatesAsync(GetAvailableDatesRequestDto dto, CancellationToken ct = default)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var to = today.AddDays(30);

            var appointments = await _unitOfWork.AppointmentRepository.GetByDateRangeAndDoctorAsync(today, to, dto.DoctorId, ct);

            var requiredSlots = dto.ServiceType.GetRequiredSlots();
            var allSlots = GenerateAllSlots(requiredSlots).ToList();
            var result = new List<DateOnly>();

            var appointmentsByDate = appointments
                .GroupBy(a => a.Date)
                .ToDictionary(g => g.Key, g => g.ToList());

            for (int i = 0; i < 30; i++)
            {
                var date = today.AddDays(i);

                List<Appointment> apointments;

                if (appointmentsByDate.TryGetValue(date, out var list))
                    apointments = list;
                else
                    apointments = [];

                var busySlots = GetBusySlots(appointments);

                if (HasFreeSlot(allSlots, busySlots, requiredSlots))
                    result.Add(date);
            }

            return result;
        }
        private bool HasFreeSlot(IEnumerable<TimeOnly> allSlots, HashSet<TimeOnly> busySlots, int requiredSlots)
        {
            return allSlots.Any(slot => Enumerable
                .Range(0, requiredSlots)
                .All(j => !busySlots.Contains(slot.AddMinutes(j * 10))));
        }
        private static HashSet<TimeOnly> GetBusySlots(IEnumerable<Appointment> appointments)
        {
            return appointments
                .SelectMany(a => Enumerable
                    .Range(0, (int)a.Duration.TotalMinutes / 10)
                    .Select(i => a.Time.AddMinutes(i * 10)))
                .ToHashSet();
        }
        private static IEnumerable<TimeOnly> GenerateAllSlots(int requiredSlots)
        {
            var slot = new TimeOnly(8, 0);
            var end = new TimeOnly(20, 0).AddMinutes(-(requiredSlots - 1) * 10);
            while (slot < end)
            {
                yield return slot;
                slot = slot.AddMinutes(10);
            }
        }
    }
}

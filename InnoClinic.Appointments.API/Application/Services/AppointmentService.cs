using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);

            appointment.Id = Guid.NewGuid();
            appointment.Status = Domain.Enums.AppointmentStatus.Created;
            appointment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AppointmentResponseDto>(appointment);
        }
    }
}

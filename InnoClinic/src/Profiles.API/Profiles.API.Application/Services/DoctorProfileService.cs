using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class DoctorProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>
    {
        private readonly IProfilesUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<DoctorProfileDto> CreateAsync(CreateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var doctorProfile = _mapper.Map<DoctorProfile>(dto);
            await _unitOfWork.DoctorProfilesRepository.CreateAsync(doctorProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<DoctorProfileDto>(doctorProfile);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var doctorProfile = await _unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));
            
            await _unitOfWork.DoctorProfilesRepository.DeleteAsync(id, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<DoctorProfileDto>> GetAllAsync(CancellationToken ct = default)
        {
            var doctorProfiles = await _unitOfWork.DoctorProfilesRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<DoctorProfileDto>>(doctorProfiles);
        }

        public async Task<DoctorProfileDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var doctorProfile = await _unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));

            return _mapper.Map<DoctorProfileDto>(doctorProfile);
        }

        public async Task<DoctorProfileDto> UpdateAsync(Guid id, UpdateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var doctorProfile = await _unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));

            _mapper.Map(dto, doctorProfile);
            await _unitOfWork.DoctorProfilesRepository.UpdateAsync(doctorProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<DoctorProfileDto>(doctorProfile);
        }
    }
}

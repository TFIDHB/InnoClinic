using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class PatientProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>
    {
        private readonly IProfilesUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<PatientProfileDto> CreateAsync(CreatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var patientProfile = _mapper.Map<PatientProfile>(dto);
            await _unitOfWork.PatientProfilesRepository.CreateAsync(patientProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var patientProfile = await _unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            await _unitOfWork.PatientProfilesRepository.DeleteAsync(id, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<PatientProfileDto>> GetAllAsync(CancellationToken ct = default)
        {
            var patientProfiles = await _unitOfWork.PatientProfilesRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<PatientProfileDto>>(patientProfiles);
        }

        public async Task<PatientProfileDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var patientProfile = await _unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            return _mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task<PatientProfileDto> UpdateAsync(Guid id, UpdatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var patientProfile = await _unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            _mapper.Map(dto, patientProfile);
            await _unitOfWork.PatientProfilesRepository.UpdateAsync(patientProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<PatientProfileDto>(patientProfile);
        }
    }
}

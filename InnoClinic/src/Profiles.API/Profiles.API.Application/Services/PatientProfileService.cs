using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class PatientProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>
    {
        public async Task<PatientProfileDto> CreateAsync(CreatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var patientProfile = mapper.Map<PatientProfile>(dto);
            patientProfile.IsLinkedToAccount = false;
            await unitOfWork.PatientProfilesRepository.CreateAsync(patientProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var patientProfile = await unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            await unitOfWork.PatientProfilesRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<PatientProfileDto>> GetAllAsync(CancellationToken ct = default)
        {
            var patientProfiles = await unitOfWork.PatientProfilesRepository.GetAllAsync(ct);
            return mapper.Map<IEnumerable<PatientProfileDto>>(patientProfiles);
        }

        public async Task<PatientProfileDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var patientProfile = await unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            return mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task<PatientProfileDto> UpdateAsync(Guid id, UpdatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var patientProfile = await unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            mapper.Map(dto, patientProfile);
            await unitOfWork.PatientProfilesRepository.UpdateAsync(patientProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(patientProfile);
        }
    }
}

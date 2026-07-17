using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class PatientProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IPatientProfileService
    {
        public async Task<PatientProfileDto> CreateAsync(CreatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var patientProfile = mapper.Map<PatientProfile>(dto);
            patientProfile.IsLinkedToAccount = false;
            await unitOfWork.PatientProfilesRepository.CreateAsync(patientProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task<PatientProfileDto> CreateOrMatchProfileAsync(
            Guid accountId,
            CreatePatientProfileRequestDto dto,
            CancellationToken ct = default)
        {
            var candidates = await unitOfWork.PatientProfilesRepository.GetUnlinkedProfilesAsync(ct);

            var bestMatch = candidates
                .Select(p => new { Profile = p, Score = CalculateMatchScore(p, dto) })
                .Where(x => x.Score >= 13)
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            if (bestMatch != null)
            {
                return mapper.Map<PatientProfileDto>(bestMatch.Profile);
            }

            var newProfile = mapper.Map<PatientProfile>(dto);
            newProfile.AccountId = accountId;
            newProfile.IsLinkedToAccount = true;

            await unitOfWork.PatientProfilesRepository.CreateAsync(newProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(newProfile);
        }

        private static int CalculateMatchScore(PatientProfile profile, CreatePatientProfileRequestDto dto)
        {
            var score = 0;
            if (string.Equals(profile.FirstName, dto.FirstName, StringComparison.OrdinalIgnoreCase)) score += 5;
            if (string.Equals(profile.LastName, dto.LastName, StringComparison.OrdinalIgnoreCase)) score += 5;
            if (string.Equals(profile.MiddleName, dto.MiddleName, StringComparison.OrdinalIgnoreCase)) score += 5;
            if (profile.DateOfBirth == dto.DateOfBirth) score += 3;
            return score;
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

        public async Task<AccountProfileInfoDto?> GetProfileInfoByAccountIdAsync(Guid id, CancellationToken ct = default)
        {
            var patienProfile = await unitOfWork.PatientProfilesRepository.GetByAccountIdAsync(id, ct);

            if (patienProfile == null)
                return null;

            return new AccountProfileInfoDto { Role = "Patient" };
        }

        public async Task<PatientProfileDto> UpdateAsync(
            Guid id,
            UpdatePatientProfileRequestDto dto,
            CancellationToken ct = default)
        {
            var patientProfile = await unitOfWork.PatientProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            mapper.Map(dto, patientProfile);
            await unitOfWork.PatientProfilesRepository.UpdateAsync(patientProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(patientProfile);
        }

        public async Task<PatientProfileDto> LinkProfileToAccountAsync(
            Guid profileId,
            Guid accountId,
            CancellationToken ct = default)
        {
            var profile = await unitOfWork.PatientProfilesRepository.GetByIdAsync(profileId, ct)
                ?? throw new NotFoundException(nameof(PatientProfile));

            profile.AccountId = accountId;
            profile.IsLinkedToAccount = true;

            await unitOfWork.PatientProfilesRepository.UpdateAsync(profile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<PatientProfileDto>(profile);
        }

        public async Task<PatientProfileDto> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default)
        {
            var patientProfile = await unitOfWork.PatientProfilesRepository.GetByAccountIdAsync(accountId, ct)
                    ?? throw new NotFoundException(nameof(PatientProfile));

            return mapper.Map<PatientProfileDto>(patientProfile);
        }
    }
}

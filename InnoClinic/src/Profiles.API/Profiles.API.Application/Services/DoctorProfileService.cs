using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class DoctorProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>
    {
        public async Task<DoctorProfileDto> CreateAsync(CreateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var doctorProfile = mapper.Map<DoctorProfile>(dto);
            await unitOfWork.DoctorProfilesRepository.CreateAsync(doctorProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<DoctorProfileDto>(doctorProfile);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var doctorProfile = await unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));

            await unitOfWork.DoctorProfilesRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<DoctorProfileDto>> GetAllAsync(CancellationToken ct = default)
        {
            var doctorProfiles = await unitOfWork.DoctorProfilesRepository.GetAllAsync(ct);
            return mapper.Map<IEnumerable<DoctorProfileDto>>(doctorProfiles);
        }

        public async Task<DoctorProfileDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var doctorProfile = await unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));

            return mapper.Map<DoctorProfileDto>(doctorProfile);
        }

        public async Task<DoctorProfileDto> UpdateAsync(Guid id, UpdateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var doctorProfile = await unitOfWork.DoctorProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(DoctorProfile));

            mapper.Map(dto, doctorProfile);
            await unitOfWork.DoctorProfilesRepository.UpdateAsync(doctorProfile, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<DoctorProfileDto>(doctorProfile);
        }
    }
}

using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class ReceptionistProfileService(IProfilesUnitOfWork unitOfWork, IMapper mapper) : IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto>
    {
        private readonly IProfilesUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ReceptionistProfileDto> CreateAsync(CreateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var receptionistProfile = _mapper.Map<ReceptionistProfile>(dto);
            await _unitOfWork.ReceptionistProfilesRepository.CreateAsync(receptionistProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<ReceptionistProfileDto>(receptionistProfile);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var receptionistProfile = await _unitOfWork.ReceptionistProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(ReceptionistProfile));

            await _unitOfWork.ReceptionistProfilesRepository.DeleteAsync(id, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<ReceptionistProfileDto>> GetAllAsync(CancellationToken ct = default)
        {
            var receptionistProfiles = await _unitOfWork.ReceptionistProfilesRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<ReceptionistProfileDto>>(receptionistProfiles);
        }

        public async Task<ReceptionistProfileDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var receptionistProfile = await _unitOfWork.ReceptionistProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(ReceptionistProfile));

            return _mapper.Map<ReceptionistProfileDto>(receptionistProfile);
        }

        public async Task<ReceptionistProfileDto> UpdateAsync(Guid id, UpdateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var receptionistProfile = await _unitOfWork.ReceptionistProfilesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(ReceptionistProfile));

            _mapper.Map(dto, receptionistProfile);
            await _unitOfWork.ReceptionistProfilesRepository.UpdateAsync(receptionistProfile, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<ReceptionistProfileDto>(receptionistProfile);
        }
    }
}

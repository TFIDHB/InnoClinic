using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class OfficesService(IOfficesUnitOfWork unitOfWork, IMapper mapper) : IOfficesService
    {
        private readonly IOfficesUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<OfficeDto> CreateAsync(CreateOfficeRequestDto dto, CancellationToken ct = default)
        {
            var office = _mapper.Map<Office>(dto);
            office.Id = Guid.NewGuid();
            await _unitOfWork.OfficesRepository.CreateAsync(office, ct);
            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<OfficeDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var office = await _unitOfWork.OfficesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Office));
            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<IEnumerable<OfficeDto>> GetAllAsync(CancellationToken ct = default)
        {
            var offices = await _unitOfWork.OfficesRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<OfficeDto>>(offices);
        }

        public async Task<OfficeDto> UpdateAsync(Guid id, UpdateOfficeRequestDto dto, CancellationToken ct = default)
        {
            var office = await _unitOfWork.OfficesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Office));
            _mapper.Map(dto, office);
            await _unitOfWork.OfficesRepository.UpdateAsync(office, ct);
            return _mapper.Map<OfficeDto>(office);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var office = await _unitOfWork.OfficesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Office));
            await _unitOfWork.OfficesRepository.DeleteAsync(id, ct);
        }
    }
}

using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class ServicesService(IServicesUnitOfWork unitOfWork, IMapper mapper) : IServicesService
    {
        private readonly IServicesUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ServiceDto> CreateAsync(CreateServiceRequestDto dto, CancellationToken ct = default)
        {
            var service = _mapper.Map<Service>(dto);
            await _unitOfWork.ServicesRepository.CreateAsync(service, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<ServiceDto>(service);
        }
        public async Task<ServiceDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var service = await _unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));

            return _mapper.Map<ServiceDto>(service);
        }
        public async Task<IEnumerable<ServiceDto>> GetAllAsync(CancellationToken ct = default)
        {
            var services = await _unitOfWork.ServicesRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }
        public async Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceRequestDto dto, CancellationToken ct = default)
        {
            var service = await _unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));

            _mapper.Map(dto, service);
            await _unitOfWork.ServicesRepository.UpdateAsync(service, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return _mapper.Map<ServiceDto>(service);
        }
        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var service = await _unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));

            await _unitOfWork.ServicesRepository.DeleteAsync(id, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}

using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class ServicesService(IServicesUnitOfWork unitOfWork, IMapper mapper) : IServicesService
    {
        public async Task<ServiceDto> CreateAsync(CreateServiceRequestDto dto, CancellationToken ct = default)
        {
            var specialization = await unitOfWork.SpecializationsRepository.GetByIdAsync(dto.SpecializationId, ct)
                ?? throw new NotFoundException(nameof(Specialization));
            var categoryExists = await unitOfWork.ServicesRepository.CategoryExistsAsync(dto.ServiceCategoryId, ct);
            if (!categoryExists)
            {
                throw new NotFoundException(nameof(ServiceCategory));
            }

            var service = mapper.Map<Service>(dto);
            await unitOfWork.ServicesRepository.CreateAsync(service, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var service = await unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));

            return mapper.Map<ServiceDto>(service);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync(CancellationToken ct = default)
        {
            var services = await unitOfWork.ServicesRepository.GetAllAsync(ct);
            return mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceRequestDto dto, CancellationToken ct = default)
        {
            var service = await unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));
            var specialization = await unitOfWork.SpecializationsRepository.GetByIdAsync(dto.SpecializationId, ct)
                ?? throw new NotFoundException(nameof(Specialization));
            var categoryExists = await unitOfWork.ServicesRepository.CategoryExistsAsync(dto.ServiceCategoryId, ct);
            if (!categoryExists)
            {
                throw new NotFoundException(nameof(ServiceCategory));
            }

            mapper.Map(dto, service);
            await unitOfWork.ServicesRepository.UpdateAsync(service, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return mapper.Map<ServiceDto>(service);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var service = await unitOfWork.ServicesRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Service));

            await unitOfWork.ServicesRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}

using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPatientProfileService : IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>
    {
        Task<PatientProfileDto> CreateOrMatchProfileAsync(Guid accountId, CreatePatientProfileRequestDto dto, CancellationToken ct = default);
        Task<PatientProfileDto> LinkProfileToAccountAsync(Guid profileId, Guid accountId, CancellationToken ct = default);
    }
}

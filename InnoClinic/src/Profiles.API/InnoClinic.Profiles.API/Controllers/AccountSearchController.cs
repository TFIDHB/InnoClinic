using Application.DTOs;
using Application.Interfaces;
using InnoClinic.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    [Authorize(Roles = Roles.InternalService)]
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountSearchController(
        IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto> patientProfilesService,
        IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto> doctorProfilesService,
        IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto> receptionistProfilesService
    ) : ControllerBase
    {
        [HttpGet("{id}/profile-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccountProfileInfoDto>> GetProfileInfo(Guid id, CancellationToken ct = default)
        {
            var result = await doctorProfilesService.GetProfileInfoByAccountIdAsync(id, ct)
                ?? await patientProfilesService.GetProfileInfoByAccountIdAsync(id, ct)
                ?? await receptionistProfilesService.GetProfileInfoByAccountIdAsync(id, ct);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

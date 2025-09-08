using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        private readonly IAppointmentService _appointmentService;

        private readonly IOperationService _operationService;

        private readonly IPatientService _patientService;

        public DoctorController(IDoctorService doctorService, IAppointmentService appointmentService, IOperationService operationService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _appointmentService = appointmentService;
            _operationService = operationService;
            _patientService = patientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] DoctorFilterDTO filterDTO)
        {
            var result = await _doctorService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _doctorService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateDoctorDTO createDoctorDTO)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var adminId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createDoctorDTO.CreatedByAdminID = adminId;

            var result = await _doctorService.AddAsync(createDoctorDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            updateDoctorDTO.ID = id;

            var result = await _doctorService.UpdateAsync(updateDoctorDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAppointments([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] AppointmentFilterDTO filterDTO)
        {
            filterDTO.DoctorID = id;

            var result = await _appointmentService.GetAllByDoctorIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/operations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetOperations([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] OperationFilterDTO filterDTO)
        {
            var result = await _operationService.GetAllByDoctorIDAsync(id, filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAllForDoctor([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] PatientFilterDTO filterDTO)
        {
            var result = await _patientService.GetAllForDoctorAsync(id, filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/leave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Leave([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _doctorService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
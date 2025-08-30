using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await _doctorService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateDoctorDTO createDoctorDTO)
        {
            var result = await _doctorService.AddAsync(createDoctorDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            if (id != updateDoctorDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _doctorService.UpdateAsync(updateDoctorDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointments(int doctorId, AppointmentFilterDTO filterDTO)
        {
            var result = await _appointmentService.GetAllByDoctorIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/operations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperations(int doctorId, OperationFilterDTO filterDTO)
        {
            var result = await _operationService.GetAllByDoctorIDAsync(doctorId, filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllForDoctor(int doctorId, [FromQuery] PatientFilterDTO filterDTO)
        {
            var result = await _patientService.GetAllForDoctorAsync(doctorId, filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/leave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Leave([FromRoute]int id)
        {
            var result = await _doctorService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
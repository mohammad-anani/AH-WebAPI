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
    public class PatientController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        private readonly IPatientService _patientService;

        private readonly IAppointmentService _appointmentService;

        private readonly ITestAppointmentService _testAppointmentService;

        private readonly IOperationService _operationService;

        public PatientController(IPatientService patientService, IInsuranceService insuranceService, IAppointmentService appointmentService, IOperationService operationService, ITestAppointmentService testAppointmentService)
        {
            _patientService = patientService;
            _insuranceService = insuranceService;
            _appointmentService = appointmentService;
            _operationService = operationService;
            _testAppointmentService = testAppointmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PatientFilterDTO filterDTO)
        {
            var result = await _patientService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/insurances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInsurances([FromQuery] InsuranceFilterDTO filterDTO, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.GetAllByPatientIDAsync(filterDTO, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointments(AppointmentFilterDTO filterDTO)
        {
            var result = await _appointmentService.GetAllByPatientIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/operations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOperations(OperationFilterDTO filterDTO)
        {
            var result = await _operationService.GetAllByPatientIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/test-appointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTestAppointments(TestAppointmentFilterDTO filterDTO)
        {
            var result = await _testAppointmentService.GetAllByPatientIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await _patientService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreatePatientDTO createPatientDTO)
        {
            var result = await _patientService.AddAsync(createPatientDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdatePatientDTO updatePatientDTO)
        {
            if (id != updatePatientDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _patientService.UpdateAsync(updatePatientDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _patientService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
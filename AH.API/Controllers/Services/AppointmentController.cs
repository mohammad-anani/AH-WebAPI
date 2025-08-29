using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AppointmentFilterDTO filterDTO)
        {
            var result = await _appointmentService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAllByDoctorId(int doctorId)
        {
            var result = await _appointmentService.GetAllByDoctorIDAsync(doctorId);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAllByPatientId(int patientId)
        {
            var result = await _appointmentService.GetAllByPatientIDAsync(patientId);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            var result = await _appointmentService.AddAsync(createAppointmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentDTO updateAppointmentDTO)
        {
            if (id != updateAppointmentDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _appointmentService.UpdateAsync(updateAppointmentDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
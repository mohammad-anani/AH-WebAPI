using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PatientFilterDTO filterDTO)
        {
            var result = await _patientService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAllForDoctor(int doctorId, [FromQuery] PatientFilterDTO filterDTO)
        {
            var result = await _patientService.GetAllForDoctorAsync(doctorId, filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _patientService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePatientDTO createPatientDTO)
        {
            var result = await _patientService.AddAsync(createPatientDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDTO updatePatientDTO)
        {
            if (id != updatePatientDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _patientService.UpdateAsync(updatePatientDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
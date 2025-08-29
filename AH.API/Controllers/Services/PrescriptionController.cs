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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetAllByAppointmentId([FromQuery] PrescriptionFilterDTO filterDTO)
        {
            var result = await _prescriptionService.GetAllByAppointmentIDAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _prescriptionService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePrescriptionDTO createPrescriptionDTO)
        {
            var result = await _prescriptionService.AddAsync(createPrescriptionDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePrescriptionDTO updatePrescriptionDTO)
        {
            if (id != updatePrescriptionDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _prescriptionService.UpdateAsync(updatePrescriptionDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _prescriptionService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
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
    public class TestAppointmentController : ControllerBase
    {
        private readonly ITestAppointmentService _testAppointmentService;

        public TestAppointmentController(ITestAppointmentService testAppointmentService)
        {
            _testAppointmentService = testAppointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TestAppointmentFilterDTO filterDTO)
        {
            var result = await _testAppointmentService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testAppointmentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTestAppointmentDTO createTestAppointmentDTO)
        {
            var result = await _testAppointmentService.AddAsync(createTestAppointmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTestAppointmentDTO updateTestAppointmentDTO)
        {
            if (id != updateTestAppointmentDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _testAppointmentService.UpdateAsync(updateTestAppointmentDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testAppointmentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
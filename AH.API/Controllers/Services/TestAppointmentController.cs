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
            try
            {
                var result = await _testAppointmentService.GetAllAsync(filterDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _testAppointmentService.GetByIDAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TestAppointment testAppointment)
        {
            try
            {
                var result = await _testAppointmentService.AddAsync(testAppointment);
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TestAppointment testAppointment)
        {
            try
            {
                var result = await _testAppointmentService.UpdateAsync(testAppointment);
                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _testAppointmentService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
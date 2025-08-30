using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class TestOrderController : ControllerBase
    {
        private readonly ITestOrderService _testOrderService;

        private readonly ITestAppointmentService _testAppointmentService;

        public TestOrderController(ITestOrderService testOrderService, ITestAppointmentService testAppointmentService)
        {
            _testOrderService = testOrderService;
            _testAppointmentService = testAppointmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testOrderService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTestOrderDTO createTestOrderDTO)
        {
            var result = await _testOrderService.AddAsync(createTestOrderDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost("{id}/reserve")]
        public async Task<IActionResult> Reserve([FromBody] CreateTestAppointmentFromTestOrderDTO createTestAppointmentDTO)
        {
            var result = await _testAppointmentService.AddFromTestOrderAsync(createTestAppointmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testOrderService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await _testOrderService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateTestOrderDTO createTestOrderDTO)
        {
            var result = await _testOrderService.AddAsync(createTestOrderDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPatch("{id}/reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reserve([FromRoute]int id, [FromBody] CreateTestAppointmentFromTestOrderDTO createTestAppointmentDTO)
        {
            // Route ID authoritative
            createTestAppointmentDTO.TestOrderID = id;
            var result = await _testAppointmentService.AddFromTestOrderAsync(createTestAppointmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _testOrderService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
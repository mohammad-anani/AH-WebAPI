using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _testOrderService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Add([FromBody] CreateTestOrderDTO createTestOrderDTO)
        {
            var result = await _testOrderService.AddAsync(createTestOrderDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("{id}/reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Reserve([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CreateTestAppointmentFromTestOrderDTO createTestAppointmentDTO)
        {

            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var receptionistId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createTestAppointmentDTO.CreatedByReceptionistID = receptionistId;


            // Route ID authoritative
            createTestAppointmentDTO.TestOrderID = id;
            var result = await _testAppointmentService.AddFromTestOrderAsync(createTestAppointmentDTO);

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
            var result = await _testOrderService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
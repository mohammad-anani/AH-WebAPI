using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class TestAppointmentController : ControllerBase
    {
        private readonly ITestAppointmentService _testAppointmentService;
        private readonly IPaymentService _paymentService;

        public TestAppointmentController(ITestAppointmentService testAppointmentService, IPaymentService paymentService)
        {
            _testAppointmentService = testAppointmentService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] TestAppointmentFilterDTO filterDTO)
        {
            var result = await _testAppointmentService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _testAppointmentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateTestAppointmentDTO createTestAppointmentDTO)
        {
            var result = await _testAppointmentService.AddAsync(createTestAppointmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateTestAppointmentDTO updateTestAppointmentDTO)
        {
            updateTestAppointmentDTO.ID = id;

            var result = await _testAppointmentService.UpdateAsync(updateTestAppointmentDTO.ToTestAppointment());

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayments([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] PaymentFilterDTO filterDTO)
        {
            var result = await _paymentService.GetAllByBillIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _testAppointmentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Start([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] StartServiceDTO dto)
        {
            var result = await _testAppointmentService.StartAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancel([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CancelServiceDTO dto)
        {
            var result = await _testAppointmentService.CancelAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CompleteServiceDTO dto)
        {
            var result = await _testAppointmentService.CompleteAsync(id, dto?.Notes, dto?.Result ?? string.Empty);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/reschedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reschedule([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] RescheduleServiceDTO dto)
        {
            var result = await _testAppointmentService.RescheduleAsync(id, dto?.Notes, dto!.NewScheduledDate);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
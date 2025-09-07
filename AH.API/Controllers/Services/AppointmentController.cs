using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        private readonly IPrescriptionService _prescriptionService;

        private readonly ITestOrderService _testOrderService;

        private readonly IPaymentService _paymentService;

        public AppointmentController(IAppointmentService appointmentService, IPrescriptionService prescriptionService, ITestOrderService testOrderService, IPaymentService paymentService)
        {
            _appointmentService = appointmentService;
            _prescriptionService = prescriptionService;
            _testOrderService = testOrderService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist,Admin")]
        public async Task<IActionResult> GetAll([FromQuery] AppointmentFilterDTO filterDTO)
        {
            var result = await _appointmentService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/prescriptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetPrescriptions([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] PrescriptionFilterDTO filterDTO)
        {
            filterDTO.AppointmentID = id;
            var result = await _prescriptionService.GetAllByAppointmentIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/test-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetTestOrders([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] TestOrderFilterDTO filterDTO)
        {
            filterDTO.AppointmentID = id;
            var result = await _testOrderService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _appointmentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Add([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var receptionistId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createAppointmentDTO.CreatedByReceptionistID = receptionistId;

            var result = await _appointmentService.AddAsync(createAppointmentDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/reserve-follow-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> AddFromPreviousAppointment([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CreateAppointmentFromPreviousDTO createAppointmentDTO)
        {
            createAppointmentDTO.AppointmentID = id;
            var result = await _appointmentService.AddFromPreviousAppointmentAsync(createAppointmentDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        //to recheck
        [HttpGet("{id}/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist,Admin,Patient")]
        public async Task<IActionResult> GetPayments([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] ServicePaymentsDTO filterDTO)
        {
            filterDTO.ID = id;
            var result = await _appointmentService.GetPaymentsAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateAppointmentDTO updateAppointmentDTO)
        {
            updateAppointmentDTO.ID = id;

            var result = await _appointmentService.UpdateAsync(updateAppointmentDTO);

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
            var result = await _appointmentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Start([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] StartServiceDTO dto)
        {
            var result = await _appointmentService.StartAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Cancel([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CancelServiceDTO dto)
        {
            var result = await _appointmentService.CancelAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Complete([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CompleteServiceDTO dto)
        {
            var result = await _appointmentService.CompleteAsync(id, dto?.Notes, dto?.Result ?? string.Empty);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/reschedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Reschedule([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] RescheduleServiceDTO dto)
        {
            var result = await _appointmentService.RescheduleAsync(id, dto?.Notes, dto!.ScheduledDate);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
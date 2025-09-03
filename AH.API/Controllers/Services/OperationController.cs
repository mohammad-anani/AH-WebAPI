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
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        private readonly IOperationDoctorService _operationDoctorService;
        private readonly IPaymentService _paymentService;

        public OperationController(IOperationService operationService, IOperationDoctorService operationDoctorService, IPaymentService paymentService)
        {
            _operationService = operationService;
            _operationDoctorService = operationDoctorService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] OperationFilterDTO filterDTO)
        {
            var result = await _operationService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _operationService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateOperationDTO createOperationDTO)
        {
            var subClaim = User.FindFirst("sub");
            if (subClaim == null)
                return Unauthorized("Missing 'sub' claim in token.");
            if (!int.TryParse(subClaim.Value, out var receptionistId))
                return Unauthorized("Invalid 'sub' claim in token.");

            createOperationDTO.CreatedByReceptionistID = receptionistId;

            var result = await _operationService.AddAsync(createOperationDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateOperationDTO updateOperationDTO)
        {
            updateOperationDTO.ID = id;

            var result = await _operationService.UpdateAsync(updateOperationDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/doctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctors([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] OperationDoctorFilterDTO filterDTO)
        {
            filterDTO.OperationID = id;
            var result = await _operationDoctorService.GetAllByOperationIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayments([FromRoute, Range(1, int.MaxValue)] int id, [FromQuery] ServicePaymentsDTO filterDTO)
        {
            filterDTO.ID = id;
            var result = await _operationService.GetPaymentsAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _operationService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Start([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] StartServiceDTO dto)
        {
            var result = await _operationService.StartAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancel([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CancelServiceDTO dto)
        {
            var result = await _operationService.CancelAsync(id, dto?.Notes);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Complete([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] CompleteServiceDTO dto)
        {
            var result = await _operationService.CompleteAsync(id, dto?.Notes, dto?.Result ?? string.Empty);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/reschedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reschedule([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] RescheduleServiceDTO dto)
        {
            var result = await _operationService.RescheduleAsync(id, dto?.Notes, dto!.NewScheduledDate);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
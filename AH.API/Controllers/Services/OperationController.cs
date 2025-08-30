using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _operationService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateOperationDTO createOperationDTO)
        {
            var result = await _operationService.AddAsync(createOperationDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOperationDTO updateOperationDTO)
        {
            if (id != updateOperationDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _operationService.UpdateAsync(updateOperationDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/doctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctors([FromQuery] OperationDoctorFilterDTO filterDTO)
        {
            var result = await _operationDoctorService.GetAllByOperationIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}/payments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayments([FromQuery] PaymentFilterDTO filterDTO)
        {
            var result = await _paymentService.GetAllByBillIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operationService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("bill/{billId}")]
        public async Task<IActionResult> GetAllByBillId([FromQuery] PaymentFilterDTO filterDTO)
        {
            var result = await _paymentService.GetAllByBillIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _paymentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePaymentDTO createPaymentDTO)
        {
            var result = await _paymentService.AddAsync(createPaymentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _paymentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationDoctorController : ControllerBase
    {
        private readonly IOperationDoctorService _operationDoctorService;

        public OperationDoctorController(IOperationDoctorService operationDoctorService)
        {
            _operationDoctorService = operationDoctorService;
        }

        [HttpGet("operation/{operationId}")]
        public async Task<IActionResult> GetAllByOperationId([FromQuery] OperationDoctorFilterDTO filterDTO)
        {
            var result = await _operationDoctorService.GetAllByOperationIDAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
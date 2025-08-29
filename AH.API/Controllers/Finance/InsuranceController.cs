using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] InsuranceFilterDTO filterDTO, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.GetAllByPatientIDAsync(filterDTO, cancellationToken);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.GetByIDAsync(id, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceDTO createInsuranceDTO, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.AddAsync(createInsuranceDTO, cancellationToken);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInsuranceDTO updateInsuranceDTO, CancellationToken cancellationToken)
        {
            if (id != updateInsuranceDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _insuranceService.UpdateAsync(updateInsuranceDTO, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.DeleteAsync(id, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
        public async Task<IActionResult> GetAll([FromQuery] InsuranceFilterDTO filterDTO)
        {
            var result = await _insuranceService.GetAllByPatientIDAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _insuranceService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceDTO createInsuranceDTO)
        {
            var result = await _insuranceService.AddAsync(createInsuranceDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInsuranceDTO updateInsuranceDTO)
        {
            if (id != updateInsuranceDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _insuranceService.UpdateAsync(updateInsuranceDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _insuranceService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
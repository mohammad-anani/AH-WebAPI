using AH.Application.DTOs.Create;
using AH.Application.DTOs.Update;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.IServices;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OperationFilterDTO filterDTO)
        {
            var result = await _operationService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _operationService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationDTO createOperationDTO)
        {
            var result = await _operationService.AddAsync(createOperationDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOperationDTO updateOperationDTO)
        {
            if (id != updateOperationDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _operationService.UpdateAsync(updateOperationDTO.ToAddUpdateOperationDTO());

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operationService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
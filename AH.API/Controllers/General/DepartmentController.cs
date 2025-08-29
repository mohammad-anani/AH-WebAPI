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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DepartmentFilterDTO filterDTO)
        {
            var result = await _departmentService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _departmentService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDepartmentDTO createDepartmentDTO)
        {
            var result = await _departmentService.AddAsync(createDepartmentDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDTO updateDepartmentDTO)
        {
            if (id != updateDepartmentDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _departmentService.UpdateAsync(updateDepartmentDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departmentService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
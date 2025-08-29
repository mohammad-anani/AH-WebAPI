using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTypeController : ControllerBase
    {
        private readonly ITestTypeService _testTypeService;

        public TestTypeController(ITestTypeService testTypeService)
        {
            _testTypeService = testTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TestTypeFilterDTO filterDTO)
        {
            var result = await _testTypeService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testTypeService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTestTypeDTO createTestTypeDTO)
        {
            var result = await _testTypeService.AddAsync(createTestTypeDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTestTypeDTO updateTestTypeDTO)
        {
            if (id != updateTestTypeDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _testTypeService.UpdateAsync(updateTestTypeDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testTypeService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
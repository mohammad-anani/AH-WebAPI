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
    public class TestOrderController : ControllerBase
    {
        private readonly ITestOrderService _testOrderService;

        public TestOrderController(ITestOrderService testOrderService)
        {
            _testOrderService = testOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TestOrderFilterDTO filterDTO)
        {
            var result = await _testOrderService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testOrderService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTestOrderDTO createTestOrderDTO)
        {
            var result = await _testOrderService.AddAsync(createTestOrderDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testOrderService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
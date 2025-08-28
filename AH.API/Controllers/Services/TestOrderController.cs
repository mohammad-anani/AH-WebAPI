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
            try
            {
                var result = await _testOrderService.GetAllAsync(filterDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _testOrderService.GetByIDAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TestOrder testOrder)
        {
            try
            {
                var result = await _testOrderService.AddAsync(testOrder);
                return CreatedAtAction(nameof(GetById), new { id = result }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _testOrderService.DeleteAsync(id);
                if (!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
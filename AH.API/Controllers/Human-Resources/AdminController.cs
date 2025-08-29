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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AdminFilterDTO filterDTO)
        {
            var result = await _adminService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, new { items = result.Data.items, count = result.Data.count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _adminService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAdminDTO createAdminDTO)
        {
            var result = await _adminService.AddAsync(createAdminDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAdminDTO updateAdminDTO)
        {
            if (id != updateAdminDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _adminService.UpdateAsync(updateAdminDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("{id}/leave")]
        public async Task<IActionResult> Leave(int id)
        {
            var result = await _adminService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
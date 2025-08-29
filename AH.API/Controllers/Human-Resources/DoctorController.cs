using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DoctorFilterDTO filterDTO)
        {
            var result = await _doctorService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDoctorDTO createDoctorDTO)
        {
            var result = await _doctorService.AddAsync(createDoctorDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            if (id != updateDoctorDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _doctorService.UpdateAsync(updateDoctorDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("{id}/leave")]
        public async Task<IActionResult> Leave(int id)
        {
            var result = await _doctorService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
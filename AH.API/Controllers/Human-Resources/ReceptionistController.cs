using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ReceptionistFilterDTO filterDTO)
        {
            var result = await _receptionistService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _receptionistService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateReceptionistDTO createReceptionistDTO)
        {
            var result = await _receptionistService.AddAsync(createReceptionistDTO);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReceptionistDTO updateReceptionistDTO)
        {
            if (id != updateReceptionistDTO.ID)
                return BadRequest("ID mismatch between route and body.");

            var result = await _receptionistService.UpdateAsync(updateReceptionistDTO.ToReceptionist());

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _receptionistService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("{id}/leave")]
        public async Task<IActionResult> Leave(int id)
        {
            var result = await _receptionistService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
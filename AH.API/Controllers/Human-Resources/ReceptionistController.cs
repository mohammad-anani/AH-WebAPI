using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _receptionistService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateReceptionistDTO createReceptionistDTO)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var adminId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createReceptionistDTO.CreatedByAdminID = adminId;

            var result = await _receptionistService.AddAsync(createReceptionistDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateReceptionistDTO updateReceptionistDTO)
        {
            updateReceptionistDTO.ID = id;

            var result = await _receptionistService.UpdateAsync(updateReceptionistDTO.ToReceptionist());

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _receptionistService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/leave")]
        public async Task<IActionResult> Leave([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _receptionistService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
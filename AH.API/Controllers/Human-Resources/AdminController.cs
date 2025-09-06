using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IJwtService jwtService;

        public AdminController(IAdminService adminService, IJwtService jwtService)
        {
            _adminService = adminService;
            this.jwtService = jwtService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] AdminFilterDTO filterDTO)
        {
            var result = await _adminService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _adminService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateAdminDTO createAdminDTO)
        {
            try
            {
                var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (subClaim == null)
                    return Unauthorized("Missing Name Identifier claim in token.");
                if (!int.TryParse(subClaim.Value, out var adminId))
                    return Unauthorized("Invalid Name Identifier claim in token.");

                createAdminDTO.CreatedByAdminID = adminId;
                var result = await _adminService.AddAsync(createAdminDTO);
                return StatusCode(result.StatusCode, result.Data);
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateAdminDTO updateAdminDTO)
        {
            updateAdminDTO.ID = id;

            var result = await _adminService.UpdateAsync(updateAdminDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var adminId))
                return Unauthorized("Invalid Name Identifier claim in token.");


            if (id == adminId)
                return StatusCode(403, "You cannot delete yourself");

            var result = await _adminService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/leave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Leave([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _adminService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
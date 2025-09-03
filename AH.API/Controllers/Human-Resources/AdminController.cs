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

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
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
        public async Task<IActionResult> Add([FromBody] CreateAdminDTO createAdminDTO)
        {
            try
            {
                // Get "sub" claim directly from User
                var subClaim = User.FindFirst("sub");
                if (subClaim == null)
                    return Unauthorized("Missing 'sub' claim in token.");

                int createdByAdminID = Convert.ToInt32(subClaim.Value);
                createAdminDTO.CreatedByAdminID = createdByAdminID;

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
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _adminService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/leave")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Leave([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _adminService.LeaveAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
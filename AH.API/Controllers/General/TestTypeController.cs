using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
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
    public class TestTypeController : ControllerBase
    {
        private readonly ITestTypeService _testTypeService;

        public TestTypeController(ITestTypeService testTypeService)
        {
            _testTypeService = testTypeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] TestTypeFilterDTO filterDTO)
        {
            var result = await _testTypeService.GetAllAsync(filterDTO);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id)
        {
            var result = await _testTypeService.GetByIDAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] CreateTestTypeDTO createTestTypeDTO)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var adminId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createTestTypeDTO.CreatedByAdminID = adminId;

            var result = await _testTypeService.AddAsync(createTestTypeDTO);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateTestTypeDTO updateTestTypeDTO)
        {
            updateTestTypeDTO.ID = id;

            var result = await _testTypeService.UpdateAsync(updateTestTypeDTO);

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
            var result = await _testTypeService.DeleteAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
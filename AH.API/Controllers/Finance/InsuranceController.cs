using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Update;
using AH.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;

namespace AH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist,Admin,Patient")]
        public async Task<IActionResult> GetById([FromRoute, Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.GetByIDAsync(id, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Add([FromBody] CreateInsuranceDTO createInsuranceDTO, CancellationToken cancellationToken)
        {
            var subClaim = User.Claims
.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subClaim == null)
                return Unauthorized("Missing Name Identifier claim in token.");
            if (!int.TryParse(subClaim.Value, out var receptionistId))
                return Unauthorized("Invalid Name Identifier claim in token.");

            createInsuranceDTO.CreatedByReceptionistID = receptionistId;

            var result = await _insuranceService.AddAsync(createInsuranceDTO, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] UpdateInsuranceDTO updateInsuranceDTO, CancellationToken cancellationToken)
        {
            updateInsuranceDTO.ID = id;

            var result = await _insuranceService.UpdateAsync(updateInsuranceDTO, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute, Range(1, int.MaxValue)] int id, CancellationToken cancellationToken)
        {
            var result = await _insuranceService.DeleteAsync(id, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPatch("{id}/renew")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Renew([FromRoute, Range(1, int.MaxValue)] int id, [FromBody] RenewInsuranceDTO renewInsuranceDTO, CancellationToken cancellationToken)
        {
            renewInsuranceDTO.ID = id;

            var result = await _insuranceService.RenewAsync(renewInsuranceDTO, cancellationToken);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
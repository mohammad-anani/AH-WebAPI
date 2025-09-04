using AH.Application.DTOs;
using AH.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers.Human_Resources
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService signinService;

        public AuthController(IAuthService signinService)
        {
            this.signinService = signinService;
        }

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SigninAsync([FromBody] SigninRequestDTO signinRequestDTO)
        {
            var result = await signinService.SigninAsync(signinRequestDTO.Email, signinRequestDTO.Password);
            return Ok(result);
        }
    }
}
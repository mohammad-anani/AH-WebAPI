using AH.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AH.API.Controllers.Human_Resources
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _PersonService;

        public PersonController(IPersonService PersonService)
        {
            _PersonService = PersonService;
        }

        [HttpPost("checkEmailAlreadyExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> CheckEmailAlreadyExists([FromBody] string email)
        {
            return Ok(await _PersonService.EmailAlreadyExists(email));
        }
    }
}
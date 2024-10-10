using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Handler.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BdJobsCorporate_CreateAccountPage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICheckNamesHandler _checkNamesHandler;

        public AccountController(ICheckNamesHandler checkNamesHandler)
        {
            _checkNamesHandler = checkNamesHandler;
        }

        // POST method for checking names
        [HttpPost("CheckNames")]
        public async Task<IActionResult> CheckNames([FromBody] CheckNamesRequestDTO request)
        {
            var response = await _checkNamesHandler.Handle(request);
            if (!string.IsNullOrEmpty(response.Message))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}

using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Handler.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BdJobsCorporate_CreateAccountPage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RLNoController : ControllerBase
    {
        private readonly IRLNoHandler _rlNoHandler;

        public RLNoController(IRLNoHandler rlNoHandler)
        {
            _rlNoHandler = rlNoHandler;
        }

        [HttpPost("CheckRLNo")]
        public async Task<IActionResult> CheckRLNo([FromBody] RLNoRequestDTO request)
        {
            var response = await _rlNoHandler.Handle(request);
            return Ok(response);
        }
    }
}

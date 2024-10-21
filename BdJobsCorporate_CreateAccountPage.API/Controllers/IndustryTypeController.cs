using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Handler.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BdJobsCorporate_CreateAccountPage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryTypeController : ControllerBase
    {
        private readonly IIndustryTypeHandler _industryTypeHandler;

        public IndustryTypeController(IIndustryTypeHandler industryTypeHandler)
        {
            _industryTypeHandler = industryTypeHandler;
        }

        [HttpPost("GetIndustryType")]
        public async Task<IActionResult> GetIndustryTypeAsync(IndustryTypeRequestDTO request)
        {
            try
            {
                var industryTypes = await _industryTypeHandler.HandleIndustryTypeAsync(request);

                var response = new
                {
                    Error = "0",
                    IndustryType = industryTypes.Any() ? (object)industryTypes : null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Error = "1",
                    Message = ex.Message
                };

                return BadRequest(errorResponse);
            }
        }

        // New GET action to retrieve all industry IDs
        [HttpGet("GetAllIndustrieIds")]
        public async Task<IActionResult> GetAllIndustrieIdsAsync()
        {
            try
            {
                var industryIds = await _industryTypeHandler.GetAllIndustrieIdsAsync();

                var response = new
                {
                    Error = "0",
                    IndustryIds = industryIds.Any() ? (object)industryIds : null
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Error = "1",
                    Message = ex.Message
                };

                return BadRequest(errorResponse);
            }
        }
    }
}

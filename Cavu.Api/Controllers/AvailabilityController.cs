using Cavu.Api.Dtos;
using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cavu.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckAvailability([FromBody] AvailabilityRequestDto request)
        {
            try
            {
                await _availabilityService.IsAvailable(request.CarParkId, request.DateFrom, request.DateTo);
                return Ok();
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
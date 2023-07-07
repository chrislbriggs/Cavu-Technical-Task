using Cavu.Api.Dtos;
using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cavu.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class QuotationsController : Controller
    {
        private readonly IAvailabilityService _availabilityService;
        private readonly IPricingService _pricingService;

        public QuotationsController(IAvailabilityService availabilityService, IPricingService pricingService)
        {
            _availabilityService = availabilityService;
            _pricingService = pricingService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateQuote([FromBody] QuotationRequestDto request)
        {
            try
            {
                await _availabilityService.IsAvailable(request.CarParkId, request.DateFrom, request.DateTo);
                var price = await _pricingService.GeneratePriceAsync(request.CarParkId, request.DateFrom, request.DateTo);
                return Ok(price);
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
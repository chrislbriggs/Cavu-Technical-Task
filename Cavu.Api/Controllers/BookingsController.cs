using Cavu.Api.Dtos;
using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cavu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(string id)
        {
            try
            {
                var booking = await _bookingService.GetByIdAsync(id);
                return Ok(new BookingResponseDto
                {
                    Id = booking.SortKey,
                    CarParkId = booking.CarParkId,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    TotalPrice = booking.TotalPrice,
                });
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
        {
            try
            {
                var booking = await _bookingService.CreateBookingAsync(request.CarParkId, request.DateFrom, request.DateTo);
                var bookingResponseDto = new BookingResponseDto
                {
                    Id = booking.SortKey,
                    CarParkId = booking.CarParkId,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    TotalPrice = booking.TotalPrice,
                };
                return Ok(bookingResponseDto);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, UpdateBookingRequestDto request)
        {
            try
            {
                var amendedBooking = await _bookingService.UpdateBookingAsync(id, request.DateFrom, request.DateTo);
                var bookingResponseDto = new BookingResponseDto
                {
                    Id = amendedBooking.SortKey,
                    CarParkId = amendedBooking.CarParkId,
                    StartDate = amendedBooking.StartDate,
                    EndDate = amendedBooking.EndDate,
                    TotalPrice = amendedBooking.TotalPrice,
                };
                return Ok(bookingResponseDto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
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
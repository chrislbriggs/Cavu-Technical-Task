using Cavu.Api.Controllers;
using Cavu.Api.Dtos;
using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Cavu.Api.UnitTests.TestData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cavu.Api.UnitTests.Controllers
{
    public class BookingsControllerTests
    {
        private readonly Mock<IBookingService> _bookingService;
        private readonly BookingsController _sut;

        public BookingsControllerTests()
        {
            _bookingService = new Mock<IBookingService>();
            _sut = new BookingsController(_bookingService.Object);
        }

        [Fact]
        public async Task CreateBooking_WithCustomException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.CreateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new CustomException("Error."));

            var response = await _sut.CreateBooking(new BookingRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<BadRequestObjectResult>(response);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task CreateBooking_WithException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.CreateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Error."));

            var response = await _sut.CreateBooking(new BookingRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<ObjectResult>(response);
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task CreateBooking_WithNoException_ReturnsOk()
        {
            _bookingService
                .Setup(x => x.CreateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(BookingTestData.Booking);

            var response = await _sut.CreateBooking(new BookingRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<OkObjectResult>(response);
            result.Value.Should().BeEquivalentTo(BookingTestData.BookingResponseDto);
            _bookingService.Verify(x => x.CreateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBooking_WithCustomException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.UpdateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new CustomException("Error."));

            var response = await _sut.UpdateBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0", new UpdateBookingRequestDto
            {
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<BadRequestObjectResult>(response);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task UpdateBooking_WithException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.UpdateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Error."));

            var response = await _sut.UpdateBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0", new UpdateBookingRequestDto
            {
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<ObjectResult>(response);
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task UpdateBooking_WithNoException_ReturnsOk()
        {
            _bookingService
                .Setup(x => x.UpdateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(BookingTestData.Booking);

            var response = await _sut.UpdateBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0", new UpdateBookingRequestDto
            {
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<OkObjectResult>(response);
            result.Value.Should().BeEquivalentTo(BookingTestData.BookingResponseDto);
            _bookingService.Verify(x => x.UpdateBookingAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBooking_WithCustomException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.DeleteBookingAsync(It.IsAny<string>()))
                .ThrowsAsync(new CustomException("Error."));

            var response = await _sut.DeleteBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0");

            var result = Assert.IsType<BadRequestObjectResult>(response);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task DeleteBooking_WithException_ReturnsBadRequest()
        {
            _bookingService
                .Setup(x => x.DeleteBookingAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error."));

            var response = await _sut.DeleteBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0");

            var result = Assert.IsType<ObjectResult>(response);
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Error.");
        }

        [Fact]
        public async Task DeleteBooking_WithNoException_ReturnsOk()
        {
            var response = await _sut.DeleteBooking("b1b6d08e-b053-4f49-a7c3-0054680ef6a0");

            Assert.IsType<OkResult>(response);
            _bookingService.Verify(x => x.DeleteBookingAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
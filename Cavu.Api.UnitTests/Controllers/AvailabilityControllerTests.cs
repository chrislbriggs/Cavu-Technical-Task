using Cavu.Api.Controllers;
using Cavu.Api.Dtos;
using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cavu.Api.UnitTests.Controllers
{
    public class AvailabilityControllerTests
    {
        private readonly Mock<IAvailabilityService> _availabilityService;
        private readonly AvailabilityController _sut;

        public AvailabilityControllerTests()
        {
            _availabilityService = new Mock<IAvailabilityService>();
            _sut = new AvailabilityController(_availabilityService.Object);
        }

        [Fact]
        public async Task CheckAvailability_WithCustomException_ReturnsBadRequest()
        {
            _availabilityService
                .Setup(x => x.IsAvailable(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new CustomException("Error."));

            var response = await _sut.CheckAvailability(new AvailabilityRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<BadRequestObjectResult>(response);
            result.Value.Should().BeEquivalentTo("Error.");
        }

        [Fact]
        public async Task CheckAvailability_WithException_ReturnsInternalServerError()
        {
            _availabilityService
                .Setup(x => x.IsAvailable(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Error."));

            var response = await _sut.CheckAvailability(new AvailabilityRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            var result = Assert.IsType<ObjectResult>(response);
            result.StatusCode.Should().Be(500);
            result.Value.Should().BeEquivalentTo("Error.");
        }

        [Fact]
        public async Task CheckAvailability_WithNoExceptions_ReturnsOk()
        {
            var response = await _sut.CheckAvailability(new AvailabilityRequestDto
            {
                CarParkId = "1234",
                DateFrom = new DateTime(2023, 07, 07),
                DateTo = new DateTime(2023, 07, 08),
            });

            Assert.IsType<OkResult>(response);
            _availabilityService.Verify(x => x.IsAvailable(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}
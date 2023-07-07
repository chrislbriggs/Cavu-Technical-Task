using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Cavu.Api.UnitTests.TestData;
using DataAccess.Repositories;
using Moq;

namespace Cavu.Api.UnitTests.Services
{
    public class AvailabilityServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<ICarParkRepository> _carParkRepository;
        private readonly AvailabilityService _sut;
        public AvailabilityServiceTests()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _carParkRepository = new Mock<ICarParkRepository>();
            _sut = new AvailabilityService(_bookingRepository.Object, _carParkRepository.Object);
        }

        [Fact]
        public async Task IsAvailable_WithPastDate_ThrowsInvalidDateSelectionException()
        {
            await Assert.ThrowsAsync<InvalidDateSelectionException>(() =>
                _sut.IsAvailable(CarParkTestData.CarPark.SortKey, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1)));
        }

        [Fact]
        public async Task IsAvailable_WithCarParkNotFound_ThrowsCarParkNotFoundException()
        {
            await Assert.ThrowsAsync<CarParkNotFoundException>(() =>
                _sut.IsAvailable(CarParkTestData.CarPark.SortKey, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2)));
        }

        [Fact]
        public async Task IsAvailable_WithMaxBookings_ThrowsInvalidDateSelectionException()
        {
            _carParkRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CarParkTestData.CarPark);
            _bookingRepository
                .Setup(x => x.GetDateFilteredBookings(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(BookingTestData.Bookings);

            await Assert.ThrowsAsync<InvalidDateSelectionException>(() =>
                _sut.IsAvailable(CarParkTestData.CarPark.SortKey, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2)));
        }
    }
}
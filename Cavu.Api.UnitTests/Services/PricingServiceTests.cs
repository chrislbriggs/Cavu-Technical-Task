using Cavu.Api.Exceptions;
using Cavu.Api.Services;
using Cavu.Api.UnitTests.TestData;
using DataAccess.Models;
using DataAccess.Repositories;
using FluentAssertions;
using Moq;

namespace Cavu.Api.UnitTests.Services
{
    public class PricingServiceTests
    {
        private readonly Mock<ICarParkRepository> _carParkRepository;
        private readonly PricingService _sut;

        public PricingServiceTests()
        {
            _carParkRepository = new Mock<ICarParkRepository>();
            _sut = new PricingService(_carParkRepository.Object);
        }

        [Fact]
        public async Task GeneratePriceAsync_WithMissingCarPark_ThrowsCarParkNotFoundException()
        {
            await Assert.ThrowsAsync<CarParkNotFoundException>(() =>
                _sut.GeneratePriceAsync(CarParkTestData.CarPark.SortKey, new DateTime(2023, 07, 07), new DateTime(2023, 07, 10)));
        }

        [Theory]
        [ClassData(typeof(PriceTestData))]
        public async Task GeneratePriceAsync_GeneratesExpectedPrices(DateTime dateFrom, DateTime dateTo, decimal expectedPrice)
        {
            _carParkRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(CarParkTestData.CarPark);

            var result = await _sut.GeneratePriceAsync(CarParkTestData.CarPark.SortKey, dateFrom, dateTo);

            result.Should().Be(expectedPrice);
        }

        [Fact]
        public async Task GeneratePriceAsync_WithMissingPrice_ThrowsPriceUnavailableException()
        {
            _carParkRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new CarPark { PricingModels = new List<PricingModel>() });

            await Assert.ThrowsAsync<PriceUnavailableException>(() =>
                _sut.GeneratePriceAsync(CarParkTestData.CarPark.SortKey, new DateTime(2023, 07, 07), new DateTime(2023, 07, 10)));
        }
    }
}
using Cavu.Api.Exceptions;
using DataAccess.Enums;
using DataAccess.Repositories;

namespace Cavu.Api.Services
{
    public class PricingService : IPricingService
    {
        private readonly ICarParkRepository _carParkRepository;

        public PricingService(ICarParkRepository carParkRepository)
        {
            _carParkRepository = carParkRepository;
        }

        public async Task<decimal> GeneratePriceAsync(string carParkId, DateTime dateFrom, DateTime dateTo)
        {
            var carPark = await _carParkRepository.GetByIdAsync(carParkId);

            if (carPark is null)
            {
                throw new CarParkNotFoundException(carParkId);
            }

            decimal totalPrice = 0;

            foreach (var day in EachCalendarDay(dateFrom, dateTo))
            {
                var pricingPeriod = GetPricingPeriodForDay(day);
                var pricingSeason = GetPricingSeasonForDay(day);
                var price = carPark.PricingModels.FirstOrDefault(x =>
                    x.PricingPeriod.Equals(pricingPeriod) && x.PricingSeason.Equals(pricingSeason));

                if (price is null)
                {
                    throw new PriceUnavailableException();
                }

                totalPrice += price.DailyRate;
            }

            return totalPrice;
        }

        private PricingPeriod GetPricingPeriodForDay(DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return PricingPeriod.Weekend;
                default:
                    return PricingPeriod.Weekday;
            }
        }

        private PricingSeason GetPricingSeasonForDay(DateTime dateTime)
        {
            var month = dateTime.Month;
            var spring = new List<int> { 3, 4, 5 };
            var summer = new List<int> { 6, 7, 8 };
            var autumn = new List<int> { 9, 10, 11 };
            var winter = new List<int> { 12, 1, 2 };

            if (spring.Contains(month))
            {
                return PricingSeason.Spring;
            }

            if (summer.Contains(month))
            {
                return PricingSeason.Summer;
            }

            if (autumn.Contains(month))
            {
                return PricingSeason.Autumn;
            }

            if (winter.Contains(month))
            {
                return PricingSeason.Winter;
            }

            throw new Exception("Invalid Month chosen.");
        }

        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
                yield
                    return date;
        }
    }
}
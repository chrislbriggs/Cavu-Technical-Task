using Cavu.Api.Exceptions;
using DataAccess.Repositories;

namespace Cavu.Api.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarParkRepository _carParkRepository;

        public AvailabilityService(IBookingRepository bookingRepository, ICarParkRepository carParkRepository)
        {
            _bookingRepository = bookingRepository;
            _carParkRepository = carParkRepository;
        }

        public async Task IsAvailable(string carParkId, DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom < DateTime.UtcNow)
            {
                throw new InvalidDateSelectionException(dateFrom, dateTo, "Starting date has to be greater than the current date.");
            }

            var carPark = await _carParkRepository.GetByIdAsync(carParkId);

            if (carPark is null)
            {
                throw new CarParkNotFoundException(carParkId);
            }
            
            var currentBookings = await _bookingRepository.GetDateFilteredBookings(carParkId, dateFrom, dateTo);

            if (currentBookings.Count.Equals(carPark.MaxAvailableSpaces))
            {
                throw new InvalidDateSelectionException(dateFrom, dateTo, "Fully booked for selected dates.");
            }

            //additional logic for checking whether booking request is available (e.g. is the space available for the entire requested duration)
        }
    }
}

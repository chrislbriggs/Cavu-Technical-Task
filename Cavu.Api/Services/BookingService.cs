using Cavu.Api.Exceptions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Cavu.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IAvailabilityService _availabilityService;
        private readonly IPricingService _pricingService;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IAvailabilityService availabilityService, IPricingService pricingService, IBookingRepository bookingRepository)
        {
            _availabilityService = availabilityService;
            _pricingService = pricingService;
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking is null)
            {
                throw new BookingNotFoundException(id);
            }

            return booking;
        }

        public async Task<Booking> CreateBookingAsync(string carParkId, DateTime dateFrom, DateTime dateTo)
        {
            await _availabilityService.IsAvailable(carParkId, dateFrom, dateTo);

            var price = await _pricingService.GeneratePriceAsync(carParkId, dateFrom, dateTo);

            var booking = new Booking
            {
                SortKey = Guid.NewGuid().ToString(),
                CarParkId = carParkId,
                StartDate = dateFrom,
                EndDate = dateTo,
                TotalPrice = price,
            };

            await _bookingRepository.SaveAsync(booking);

            return booking;
        }

        public async Task<Booking> UpdateBookingAsync(string bookingId, DateTime dateFrom, DateTime dateTo)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking is null)
            {
                throw new BookingNotFoundException(bookingId);
            }

            await _availabilityService.IsAvailable(booking.CarParkId, dateFrom, dateTo);

            var updatedPrice = await _pricingService.GeneratePriceAsync(booking.CarParkId, dateFrom, dateTo);

            booking.StartDate = dateFrom;
            booking.EndDate = dateTo;
            booking.TotalPrice = updatedPrice;

            await _bookingRepository.SaveAsync(booking);

            return booking;
        }

        public async Task DeleteBookingAsync(string bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking is null)
            {
                throw new BookingNotFoundException(bookingId);
            }

            await _bookingRepository.DeleteAsync(booking);
        }
    }
}
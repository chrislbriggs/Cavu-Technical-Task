using DataAccess.Models;

namespace Cavu.Api.Services
{
    public interface IBookingService
    {
        Task<Booking> GetByIdAsync(string id);

        Task<Booking> CreateBookingAsync(string carParkId, DateTime dateFrom, DateTime dateTo);

        Task<Booking> UpdateBookingAsync(string bookingId, DateTime dateFrom, DateTime dateTo);

        Task DeleteBookingAsync(string bookingId);
    }
}
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface IBookingRepository
    {
        Task SaveAsync(Booking booking);

        Task<Booking> GetByIdAsync(string id);

        Task DeleteAsync(Booking booking);

        Task<List<Booking>> GetDateFilteredBookings(string carParkId, DateTime dateFrom,  DateTime dateTo);
    }
}
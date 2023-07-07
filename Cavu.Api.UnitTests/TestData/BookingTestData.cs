using Cavu.Api.Dtos;
using DataAccess.Models;

namespace Cavu.Api.UnitTests.TestData
{
    internal static class BookingTestData
    {
        internal static List<Booking> Bookings => new List<Booking>
        {
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
            new Booking(),
        };

        internal static Booking Booking => new Booking
        {
            SortKey = "b1b6d08e-b053-4f49-a7c3-0054680ef6a0",
            CarParkId = "4783dd09-6366-4f8e-a182-1e96cfa90d41",
            EndDate = new DateTime(2023, 07, 08),
            StartDate = new DateTime(2023, 07, 07),
            TotalPrice = 50,
        };

        internal static BookingResponseDto BookingResponseDto => new BookingResponseDto
        {
            Id = "b1b6d08e-b053-4f49-a7c3-0054680ef6a0",
            CarParkId = "4783dd09-6366-4f8e-a182-1e96cfa90d41",
            EndDate = new DateTime(2023, 07, 08),
            StartDate = new DateTime(2023, 07, 07),
            TotalPrice = 50,
        };
    }
}
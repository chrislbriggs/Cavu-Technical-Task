using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private const string TableName = "chris-test";
        private readonly IDynamoDBContext _context;

        public BookingRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Booking booking)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            await _context.SaveAsync(booking, operationConfig);
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            return await _context.LoadAsync<Booking>(id, operationConfig);
        }

        public async Task DeleteAsync(Booking booking)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            await _context.DeleteAsync(booking, operationConfig);
        }

        public async Task<List<Booking>> GetDateFilteredBookings(string carParkId, DateTime dateFrom, DateTime dateTo)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition(nameof(Booking.CarParkId), ScanOperator.Equal, carParkId),
                    new ScanCondition(nameof(Booking.StartDate), ScanOperator.GreaterThanOrEqual, dateFrom),
                    new ScanCondition(nameof(Booking.EndDate), ScanOperator.LessThanOrEqual, dateTo)
                }
            };

            var query = _context.QueryAsync<Booking>(nameof(Booking), operationConfig);
            var bookings = await query.GetRemainingAsync();
            return bookings;
        }
    }
}
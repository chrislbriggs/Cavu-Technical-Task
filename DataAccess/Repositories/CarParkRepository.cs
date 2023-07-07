using Amazon.DynamoDBv2.DataModel;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class CarParkRepository : ICarParkRepository
    {
        private const string TableName = "chris-test";
        private readonly IDynamoDBContext _context;

        public CarParkRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CarPark carPark)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            await _context.SaveAsync(carPark, operationConfig);
        }

        public async Task<CarPark> GetByIdAsync(string id)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            return await _context.LoadAsync<CarPark>(nameof(CarPark), id, operationConfig);
        }

        public async Task<List<CarPark>> GetAllAsync()
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = TableName,
            };

            return await _context.QueryAsync<CarPark>(nameof(CarPark), operationConfig).GetRemainingAsync();
        }
    }
}
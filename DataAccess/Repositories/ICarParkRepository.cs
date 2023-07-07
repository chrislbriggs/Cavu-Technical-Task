using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICarParkRepository
    {
        Task SaveAsync(CarPark carPark);

        Task<CarPark> GetByIdAsync(string id);

        Task<List<CarPark>> GetAllAsync();
    }
}
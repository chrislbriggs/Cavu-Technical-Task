namespace Cavu.Api.Services
{
    public interface IAvailabilityService
    {
        Task IsAvailable(string carParkId, DateTime dateFrom, DateTime dateTo);
    }
}
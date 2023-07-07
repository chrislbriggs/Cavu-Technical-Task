namespace Cavu.Api.Services
{
    public interface IPricingService
    {
        Task<decimal> GeneratePriceAsync(string carParkId, DateTime dateFrom,  DateTime dateTo);
    }
}
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Services
{
    public interface IShipperService
    {
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
        Task<Shipper?> GetShipperByIdAsync(int shipperId);
    }
}
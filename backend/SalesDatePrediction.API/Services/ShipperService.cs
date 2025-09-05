using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Repositories;

namespace SalesDatePrediction.API.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            return await _shipperRepository.GetAllShippersAsync();
        }

        public async Task<Shipper?> GetShipperByIdAsync(int shipperId)
        {
            return await _shipperRepository.GetShipperByIdAsync(shipperId);
        }
    }
}
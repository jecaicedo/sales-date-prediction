using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
        Task<Shipper?> GetShipperByIdAsync(int shipperId);
    }
}
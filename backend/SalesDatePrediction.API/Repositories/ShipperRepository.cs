using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Data;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly StoreContext _context;

        public ShipperRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            return await _context.Shippers.ToListAsync();
        }

        public async Task<Shipper?> GetShipperByIdAsync(int shipperId)
        {
            return await _context.Shippers
                .FirstOrDefaultAsync(s => s.ShipperID == shipperId);
        }
    }
}
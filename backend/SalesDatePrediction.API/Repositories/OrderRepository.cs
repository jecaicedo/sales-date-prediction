using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Data;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;
        private readonly DbConnectionFactory _connectionFactory;

        public OrderRepository(StoreContext context, DbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string custId)
        {
            const string query = @"
                SELECT 
                    OrderID, 
                    RequiredDate, 
                    ShippedDate, 
                    ShipName, 
                    ShipAddress, 
                    ShipCity
                FROM [Sales].[Orders]
                WHERE custid = @CustId";

            using var connection = _connectionFactory.CreateConnection();
            var orders = await connection.QueryAsync<Order>(query, new { CustId = custId });
            return orders;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<int> CreateOrderAsync(Order order, OrderDetail orderDetail)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                orderDetail.OrderID = order.OrderID;

                await _context.OrderDetails.AddAsync(orderDetail);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order.OrderID;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

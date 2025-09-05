using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string custId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<int> CreateOrderAsync(Order order, OrderDetail orderDetail);
    }
}

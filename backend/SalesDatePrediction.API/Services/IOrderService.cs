using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string custId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<Order> CreateOrderAsync(Order order, OrderDetail orderDetail);
    }
}

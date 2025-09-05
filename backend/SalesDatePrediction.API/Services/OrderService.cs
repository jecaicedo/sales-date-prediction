using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Repositories;

namespace SalesDatePrediction.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string custId)
        {
            return await _orderRepository.GetOrdersByCustomerIdAsync(custId);
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(Order order, OrderDetail orderDetail)
    {
        int orderId = await _orderRepository.CreateOrderAsync(order, orderDetail);
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }
    }
}

using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Services;

namespace SalesDatePrediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("customer/{custId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(string custId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(custId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is required");
            }

            var order = new Order
            {
                CustID = orderDto.CustID,
                EmpID = orderDto.EmpID,
                OrderDate = DateTime.Now,
                RequiredDate = orderDto.RequiredDate,
                ShippedDate = orderDto.ShippedDate,
                ShipperID = orderDto.ShipperID,
                Freight = orderDto.Freight,
                ShipName = orderDto.ShipName,
                ShipAddress = orderDto.ShipAddress,
                ShipCity = orderDto.ShipCity,
                ShipCountry = orderDto.ShipCountry
            };

            var orderDetail = new OrderDetail
            {
                ProductID = orderDto.ProductID,
                UnitPrice = orderDto.UnitPrice,
                Qty = orderDto.Quantity,
                Discount = orderDto.Discount / 100
            };

            var createdOrder = await _orderService.CreateOrderAsync(order, orderDetail);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderID }, createdOrder);
        }
    }

    public class OrderCreateDto
    {
        public int CustID { get; set; }
        public int EmpID { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipperID { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}

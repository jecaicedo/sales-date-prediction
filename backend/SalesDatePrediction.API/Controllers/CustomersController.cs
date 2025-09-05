using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Services;

namespace SalesDatePrediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("predictions")]
        public async Task<ActionResult<IEnumerable<CustomerPrediction>>> GetCustomerPredictions([FromQuery] string? customerName = null)
        {
            var predictions = await _customerService.GetCustomerPredictionsAsync(customerName);
            return Ok(predictions);
        }
    }
}

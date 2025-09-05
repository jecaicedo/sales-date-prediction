using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Repositories;

namespace SalesDatePrediction.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerPrediction>> GetCustomerPredictionsAsync(string? customerName = null)
        {
            return await _customerRepository.GetCustomerPredictionsAsync(customerName);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int custId)
        {
            return await _customerRepository.GetCustomerByIdAsync(custId);
        }
    }
}

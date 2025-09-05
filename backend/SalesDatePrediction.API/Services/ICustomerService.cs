using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerPrediction>> GetCustomerPredictionsAsync(string? customerName = null);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int custId);
    }
}

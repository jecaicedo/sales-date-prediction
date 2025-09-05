using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Data;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerPrediction>> GetCustomerPredictionsAsync(string? customerName = null);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int custId);
    }
}

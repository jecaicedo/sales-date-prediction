using System.Collections.Generic;
using System.Threading.Tasks;
using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int empId);
    }
}
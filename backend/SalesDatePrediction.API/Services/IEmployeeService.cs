using SalesDatePrediction.API.Models;

namespace SalesDatePrediction.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int empId);
    }
}
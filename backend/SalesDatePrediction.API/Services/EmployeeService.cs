using SalesDatePrediction.API.Models;
using SalesDatePrediction.API.Repositories;

namespace SalesDatePrediction.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int empId)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(empId);
        }
    }
}